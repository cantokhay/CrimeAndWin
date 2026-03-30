using Action.Application.DTOs.ActionAttemptDTOs;
using Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction;
using Action.Application.Features.PlayerActionAttempts.Commands.Seed;
using Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerAttempts;
using MediatR;
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

        /// <summary>
        /// Tüm PlayerActionAttempt kayıtlarını listeler.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ResultPlayerActionAttemptDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPlayerActionAttemptsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele PlayerActionAttempt verilerini oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 15)
        {
            await _mediator.Send(new RunPlayerActionAttemptSeedCommand(count));
            return Ok(new { message = $"{count} adet PlayerActionAttempt başarıyla seed edildi." });
        }
    }
}
