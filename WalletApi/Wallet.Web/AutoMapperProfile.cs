using AutoMapper;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Commands;
using Wallet.Application.Features.TransactionFeatures.Commands;
using Wallet.Application.Features.TransactionFeatures.Dtos;
using Wallet.Core.Entities;

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

        /* Transactions */
        CreateMap<CreateTransactionCommand, TransactionEntity>();
        CreateMap<TransactionCategoryEntity, TransactionCategoryDto>();
        CreateMap<TransactionEntity, TransactionDto>()
             .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status.Name))
             .ForMember(dest => dest.Type, act => act.MapFrom(src => src.Type.Name));

    }
}
