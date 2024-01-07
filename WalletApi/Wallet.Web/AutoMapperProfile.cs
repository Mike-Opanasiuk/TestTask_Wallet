using AutoMapper;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Commands;
using Wallet.Application.Features.CardFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Queries;
using Wallet.Application.Features.TransactionFeatures.Commands;
using Wallet.Application.Features.TransactionFeatures.Dtos;
using Wallet.Core.Entities;
using Wallet.Shared;

namespace Wallet.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        /* Users */

        CreateMap<RegisterUserCommand, UserEntity>();
        CreateMap<UserEntity, UserDto>();

        /* Cards */

        CreateMap<CreateCardCommand, CardEntity>();
        CreateMap<CreateCardRequest, CreateCardCommand>();
        CreateMap<CardEntity, CardDto>();
        CreateMap<GetCardByIdRequest, GetCardByIdQuery>();

        /* Transactions */
        CreateMap<CreateTransactionCommand, TransactionEntity>();
        CreateMap<TransactionCategoryEntity, TransactionCategoryDto>();

        CreateMap<TransactionEntity, TransactionDto>()
             .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status.Name))
             .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type.Name))
             .ForMember(dest => dest.Sum, act => act.MapFrom(src =>
                (src.TypeId == AppConstant.TransactionTypes.Payment.Key ? "+" + src.Sum : src.Sum.ToString())))
            .ForMember(dest => dest.Date, act => act.MapFrom(src =>
                (src.CreatedOn > DateTime.Now.Subtract(TimeSpan.FromDays(7)) ? 
                                                           src.CreatedOn.DayOfWeek.ToString() : 
                                                           src.CreatedOn.ToString("dd/MM/yy"))));

    }
}
