using Action.Application.DTOs;
using Action.Application.Features.PlayerActions.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Action.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerActionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerActionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("attempt")]
        public async Task<IActionResult> Attempt([FromBody] PlayerActionAttemptDTO request)
        => Ok(await _mediator.Send(new PerformPlayerActionCommand(request)));
    }
}
