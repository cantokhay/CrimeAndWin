using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Moderation.Application.Features.ModerationAction.Commands.CreateBan;
using Moderation.Application.Features.ModerationAction.Commands.CreateRestriction;
using Moderation.Application.Features.ModerationAction.Commands.LiftRestriction;
using Moderation.Application.Features.ModerationAction.Queries.GetActionsByPlayerId;
using Moderation.Application.Features.ModerationAction.Queries.GetAllActions;

namespace Moderation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModerationActionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ModerationActionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("ban")]
        // [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Ban([FromBody] CreateBanDTO dto)
        {
            var id = await _mediator.Send(new CreateBanCommand(dto));
            return Ok(new { id });
        }

        [HttpPost("restrict")]
        // [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Restrict([FromBody] CreateRestrictDTO dto)
        {
            var id = await _mediator.Send(new CreateRestrictCommand(dto));
            return Ok(new { id });
        }

        [HttpPost("lift")]
        // [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Lift([FromBody] LiftRestrictionDTO dto)
        {
            var ok = await _mediator.Send(new LiftRestrictionCommand(dto));
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("by-player/{playerId:guid}")]
        public async Task<IActionResult> ByPlayer(Guid playerId)
        {
            var list = await _mediator.Send(new GetActionsByPlayerIdQuery(playerId));
            return Ok(list);
        }

        /// <summary>
        /// Tüm moderasyon aksiyonlarını listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultModerationActionDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllModerationActionsQuery());
            return Ok(result);
        }
    }
}
