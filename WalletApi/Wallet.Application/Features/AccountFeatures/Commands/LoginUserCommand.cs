using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Application.Features.AccountFeatures.Services;
using Wallet.Core.Entities;
using Wallet.Shared;
using Wallet.Shared.CustomExceptions;

namespace Wallet.Application.Features.AccountFeatures.Commands;

public record LoginUserCommand(string UserName, string Password) : IRequest<AuthResponse>;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly UserManager<UserEntity> userManager;
    private readonly JwtService jwtService;
    private readonly IMapper mapper;

    public LoginUserHandler(UserManager<UserEntity> userManager, JwtService jwtService, IMapper mapper)
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
        this.mapper = mapper;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            throw new BadRequestException("Invalid credentials");
        }

        bool isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new BadRequestException("Invalid credentials");
        }

        TimeSpan tokenLifetime = AppConstant.JwtTokenLifetimes.Default;

        var token = jwtService.GenerateToken(
            user.Id.ToString(),
            string.Join(", ", await userManager.GetRolesAsync(user)),
            tokenLifetime);

        return new AuthResponse()
        {
            Token = token,
            User = mapper.Map<UserDto>(user)
        };
    }
}