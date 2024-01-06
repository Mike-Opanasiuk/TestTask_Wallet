using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;
using Wallet.Application.Features.CardFeatures.Commands;
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
    public async Task RegisterAsync([FromBody] CreateCardRequest request)
    {
        var command = mapper.Map<CreateCardCommand>(request);

        command.OwnerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Claims.Id)!.Value);

        await mediator.Send(command);
    }
}
