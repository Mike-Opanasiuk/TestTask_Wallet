using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Wallet.Core.Entities;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Shared;
using Wallet.Shared.CustomExceptions;

namespace Wallet.Application.Features.TransactionFeatures.Commands;


public record CreateTransactionCommand : IRequest
{
    public decimal Sum { get; set; }
    public string? Description { get; set; }
    public Guid TypeId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid StatusId { get; set; }
    public Guid CardId { get; set; }
    public Guid AuthorizedUserId { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly UserManager<UserEntity> userManager;

    public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        await CheckIdsCorrectnessAsync(request);

        var card = await unitOfWork.Cards.FindAsync(request.CardId);

        if (request.TypeId == AppConstant.TransactionTypes.Credit.Key)
        {
            card.Balance -= request.Sum;
        }
        else
        {
            if(card.Available < request.Sum)
            {
                throw new BadRequestException($"Card limit exceeded. Max limit is {card.MaxLimit}");
            }

            card.Balance += request.Sum;
        }

        await unitOfWork.Transactions.InsertAsync(mapper.Map<TransactionEntity>(request));

        var changesCount = await unitOfWork.SaveChangesAsync();

        if (changesCount == 0)
        {
            throw new Exception("No transactions were added.");
        }
    }

    private async Task CheckIdsCorrectnessAsync(CreateTransactionCommand request)
    {
        if ((await unitOfWork.Cards.FindAsync(request.CardId)) is null)
        {
            throw new BadRequestException($"Card with id {request.CardId} was not found.");
        }

        if ((await unitOfWork.TransactionTypes.FindAsync(request.TypeId)) is null)
        {
            throw new BadRequestException($"Transaction type with id {request.TypeId} was not found.");
        }

        if ((await unitOfWork.TransactionStatuses.FindAsync(request.StatusId)) is null)
        {
            throw new BadRequestException($"Transaction status with id {request.StatusId} was not found.");
        }

        if ((await unitOfWork.TransactionCategories.FindAsync(request.CategoryId)) is null)
        {
            throw new BadRequestException($"Transaction category with id {request.CategoryId} was not found.");
        }

        if ((await userManager.FindByIdAsync(request.AuthorizedUserId.ToString())) is null)
        {
            throw new BadRequestException($"Authorized user with id {request.AuthorizedUserId} was not found.");
        }
    }
}

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(c => c.Sum).GreaterThanOrEqualTo(0);

        RuleFor(c => c.CardId).NotEmpty().NotNull();
        RuleFor(c => c.StatusId).NotEmpty().NotNull();
        RuleFor(c => c.TypeId).NotEmpty().NotNull();
        RuleFor(c => c.CategoryId).NotEmpty().NotNull();
    }
}