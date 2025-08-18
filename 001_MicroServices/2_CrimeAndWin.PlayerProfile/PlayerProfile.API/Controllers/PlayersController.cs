using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerProfile.Application.Features.Player.Commands.CreatePlayer;
using PlayerProfile.Application.Features.Player.DTOs;
using PlayerProfile.Application.Features.Player.Commands.UpdateAvatar;
using PlayerProfile.Application.Features.Player.Queries;

namespace PlayerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PlayersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreatePlayerDTO>> Create([FromBody] CreatePlayerCommand cmd)
            => Ok(await mediator.Send(cmd));

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultPlayerDTO>> GetById(Guid id)
            => Ok(await mediator.Send(new GetByIdPlayerQuery(id)));

        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(Guid id, [FromBody] UpdateAvatarRequest req)
        {
            await mediator.Send(new UpdateAvatarCommand(id, req.AvatarKey));
            return NoContent();
        }

        public sealed class UpdateAvatarRequest { public string AvatarKey { get; set; } = default!; }
    }
}
