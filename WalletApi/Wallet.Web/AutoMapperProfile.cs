using AutoMapper;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Core.Entities;

namespace Wallet.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterUserCommand, UserEntity>();
        CreateMap<UserEntity, UserDto>();
    }
}
