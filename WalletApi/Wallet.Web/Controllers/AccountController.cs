using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.AccountFeatures.Commands;
using Wallet.Application.Features.AccountFeatures.Dtos;

namespace Wallet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> RegisterAsync([FromBody] RegisterUserCommand command)
        {
            return await mediator.Send(command);
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> LoginAsync([FromBody] LoginUserCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
