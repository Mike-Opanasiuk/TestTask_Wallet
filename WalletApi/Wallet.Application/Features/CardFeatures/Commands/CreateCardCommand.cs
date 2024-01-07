using AutoMapper;
using FluentValidation;
using MediatR;
using Wallet.Core.Entities;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Shared;

namespace Wallet.Application.Features.CardFeatures.Commands;

public record CreateCardRequest
{
    public decimal Balance { get; set; }
}

public record CreateCardCommand : CreateCardRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateCardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var card = mapper.Map<CardEntity>(request);
        card.MaxLimit = AppConstant.CardLimits.Default;

        await unitOfWork.Cards.InsertAsync(card);

        var changesCount = await unitOfWork.SaveChangesAsync();

        if(changesCount == 0)
        {
            throw new Exception("No cards were added.");
        }
    }
}

public class CreateCardCommandValidator: AbstractValidator<CreateCardRequest>
{
    public CreateCardCommandValidator()
    {
        RuleFor(c => c.Balance).LessThanOrEqualTo(AppConstant.CardLimits.Default);
    }
}