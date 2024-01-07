using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Features.CardFeatures.Dtos;
using Wallet.Application.Features.TransactionFeatures.Dtos;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Shared.CustomExceptions;
using static Wallet.Shared.AppConstant;

namespace Wallet.Application.Features.CardFeatures.Queries;


public record GetCardByIdQuery : IRequest<CardDto>
{
    public Guid Id { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string? SearchString { get; set; }
    public Guid AuthorizedUserId { get; set; }
}

public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, CardDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetCardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var card = await unitOfWork.Cards
            .Get()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (card == null)
        {
            throw new BadRequestException($"Card with id {id} does not exist.");
        }

        if (card.OwnerId != request.AuthorizedUserId)
        {
            throw new BadRequestException("You don't have permission to view this card information.");
        }

        var transactions = unitOfWork.Transactions
            .Get()
            .Include(c => c.Type)
            .Include(c => c.Status)
            .Include(c => c.Category)
            .OrderByDescending(c => c.CreatedOn)
            .Where(c => c.CardId == id);

        transactions = Paginate(
           transactions,
           request.PerPage == default ? Paging.DefaultPerPage : request.PerPage,
           request.Page == default ? Paging.DefaultPage : request.Page,
           out int totalPages);

        DateTime startDateTime = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc); //Today at 00:00:00
        DateTime endDateTime = DateTime.SpecifyKind(DateTime.Today.AddDays(1).AddTicks(-1), DateTimeKind.Utc); //Today at 23:59:59

        var todayPoints = await unitOfWork.Points.Get()
            .Where(p => p.CreatedOn >= startDateTime && p.CreatedOn <= endDateTime)
            .FirstOrDefaultAsync(p => p.CardId == id);

        var cardDto = mapper.Map<CardDto>(card);

        cardDto.Transactions = mapper.Map<ICollection<TransactionDto>>(transactions);
        cardDto.TotalTransactionsPages = totalPages;

        if (todayPoints != null)
        {
            cardDto.Points = FormatPoints(todayPoints.Amount);
        }

        return cardDto;
    }

    private IQueryable<T> Paginate<T>(IQueryable<T> items, int perPage, int page, out int totalPages)
    {
        var count = items.Count();

        items = items.Skip((page - 1) * perPage).Take(perPage);

        if (count <= perPage && count != 0)
        {
            totalPages = 1;
            return items;
        }

        totalPages = count / perPage;

        if (totalPages % perPage > 0)
        {
            ++totalPages;
        }
        return items;
    }

    private string FormatPoints(decimal points)
    {
        if (points < 1000)
        {
            return Math.Round(points, 2).ToString();
        }
        else
        {
            double roundedPoints = Math.Round((double)points / 1000.0, 1);
            return $"{roundedPoints}K";
        }
    }
}