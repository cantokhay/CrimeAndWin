using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlayerProfile.Application.DTOs.PlayerDTOs;
using PlayerProfile.Application.Features.Player.Commands.CreatePlayer;
using PlayerProfile.Application.Features.Player.Commands.Seed;
using PlayerProfile.Application.Features.Player.Commands.UpdateAvatar;
using PlayerProfile.Application.Features.Player.Queries.GetAllPlayer;
using PlayerProfile.Application.Features.Player.Queries.GetByIdPlayer;

namespace PlayerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePlayerDTO>> Create([FromBody] CreatePlayerCommand cmd)
            => Ok(await _mediator.Send(cmd));

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultPlayerDTO>> GetById(Guid id)
            => Ok(await _mediator.Send(new GetByIdPlayerQuery(id)));

        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(Guid id, [FromBody] UpdateAvatarRequest req)
        {
            await _mediator.Send(new UpdateAvatarCommand(id, req.AvatarKey));
            return NoContent();
        }

        public sealed class UpdateAvatarRequest { public string AvatarKey { get; set; } = default!; }

        [HttpGet("GetAllPlayer")]
        public async Task<ActionResult<List<ResultPlayerDTO>>> GetAllPlayer() 
            => Ok(await _mediator.Send(new GetAllPlayersQuery()));

        /// <summary>
        /// Bogus ile belirtilen sayıda benzersiz Player verisi oluşturur.
        /// </summary>
        /// <param name="count">Kaç oyuncu oluşturulacak</param>
        [HttpPost("run")]
        public async Task<IActionResult> Run([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunPlayerSeedCommand(count));
            return Ok(new { message = $"{count} adet Player başarıyla seed edildi." });
        }
    }
}
