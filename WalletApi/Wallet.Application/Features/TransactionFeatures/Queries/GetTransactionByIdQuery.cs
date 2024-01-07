using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Features.TransactionFeatures.Dtos;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Shared.CustomExceptions;

namespace Wallet.Application.Features.TransactionFeatures.Queries;
public record GetTransactionByIdQuery : IRequest<TransactionDto>
{
    public Guid Id { get; set; }
    public Guid AuthorizedUserId { get; set; }
}

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var transaction = await unitOfWork.Transactions
            .Get()
            .Include(c => c.Type)
            .Include(c => c.Status)
            .Include(c => c.Category)
            .Include(c => c.AuthorizedUser)
            .Include(c => c.Card)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (transaction == null)
        {
            throw new BadRequestException($"Transaction with id {id} does not exist.");
        }

        if (transaction.AuthorizedUserId != request.AuthorizedUserId
            && transaction.Card.OwnerId != request.AuthorizedUserId)
        {
            throw new BadRequestException("You don't have permission to view this transaction.");
        }

        return mapper.Map<TransactionDto>(transaction);
    }
}