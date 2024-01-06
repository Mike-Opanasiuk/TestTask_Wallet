using AutoMapper;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Commands;
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
    }
}
