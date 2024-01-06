﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Wallet.Shared.AppConstant;

namespace Wallet.Application.Features.AccountFeatures.Services;


public class JwtService
{
    private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512Signature;
    private readonly SigningCredentials signingCredentials;
    private readonly JwtSecurityTokenHandler jwtTokenHandler;

    public JwtService(SymmetricSecurityKey securityKey)
    {
        signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithm);
        jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateToken(string userId, string userRoles, TimeSpan duration)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(Claims.Id, userId),
                new(Claims.Roles, userRoles)
            }),
            Expires = DateTime.UtcNow.Add(duration),
            SigningCredentials = signingCredentials
        };

        var token = jwtTokenHandler.CreateToken(descriptor);

        return jwtTokenHandler.WriteToken(token);
    }
}
