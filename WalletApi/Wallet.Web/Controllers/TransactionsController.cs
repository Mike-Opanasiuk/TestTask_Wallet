﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Wallet.Shared.AppConstant;
using Wallet.Application.Features.TransactionFeatures.Commands;
using Wallet.Application.Features.TransactionFeatures.Dtos;
using Wallet.Application.Features.TransactionFeatures.Queries;

namespace Wallet.Web.Controllers;


[Route("api/[controller]")]
[ApiController]

public class TransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public TransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task CreateTransactionAsync([FromBody] CreateTransactionCommand request)
    {
        if (request.AuthorizedUserId == Guid.Empty)
            request.AuthorizedUserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Claims.Id)!.Value);

        await mediator.Send(request);
    }


    [HttpGet("{transactionId}")]
    [Authorize]
    public async Task<ActionResult<TransactionDto>> GetTransactionByIdAsync([FromRoute] Guid transactionId)
    {
        return await mediator.Send(new GetTransactionByIdQuery() 
        { 
            Id = transactionId,
            AuthorizedUserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Claims.Id)!.Value)
        });
    }
}
