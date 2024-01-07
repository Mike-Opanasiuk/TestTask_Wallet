using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.CardFeatures.Commands;
using Wallet.Application.Features.CardFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Queries;
using static Wallet.Shared.AppConstant;

namespace Wallet.Web.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CardsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public CardsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task CreateCardAsync([FromBody] CreateCardRequest request)
    {
        var command = mapper.Map<CreateCardCommand>(request);

        command.OwnerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Claims.Id)!.Value);

        await mediator.Send(command);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CardDto>> GetCardByIdAsync([FromQuery] GetCardByIdRequest request)
    {
        var query = mapper.Map<GetCardByIdQuery>(request);

        query.AuthorizedUserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Claims.Id)!.Value);

        return await mediator.Send(query);
    }
}
