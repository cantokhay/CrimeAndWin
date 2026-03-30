using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;
using PlayerProfile.Application.Features.Player.Commands.AdminCreatePlayer;
using PlayerProfile.Application.Features.Player.Commands.AdminDeletePlayer;
using PlayerProfile.Application.Features.Player.Commands.AdminUpdatePlayer;
using PlayerProfile.Application.Features.Player.Commands.Seed;
using PlayerProfile.Application.Features.Player.Queries.GetAllPlayersAsAdmin;
using PlayerProfile.Application.Features.Player.Queries.GetByIdPlayerAsAdmin;

namespace PlayerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // -------------------------------
        // 📋 GET ALL PLAYERS
        // -------------------------------
        [HttpGet("GetAllPlayersAsAdmin")]
        public async Task<IActionResult> GetAllPlayersAsAdmin()
        {
            var result = await _mediator.Send(new GetAllPlayersAsAdminQuery());
            return Ok(result);
        }

        // -------------------------------
        // 🔍 GET PLAYER BY ID
        // -------------------------------
        [HttpGet("GetByIdPlayerAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetByIdPlayerAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetPlayerByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // -------------------------------
        // ➕ CREATE PLAYER (Admin)
        // -------------------------------
        [HttpPost("CreatePlayerAsAdmin")]
        public async Task<IActionResult> CreatePlayerAsAdmin([FromBody] AdminCreatePlayerDTO dto)
        {
            var command = new AdminCreatePlayerCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // -------------------------------
        // ✏️ UPDATE PLAYER (Admin)
        // -------------------------------
        [HttpPut("UpdatePlayerAsAdmin")]
        public async Task<IActionResult> UpdatePlayerAsAdmin([FromBody] AdminUpdatePlayerDTO dto)
        {
            var command = new AdminUpdatePlayerCommand(dto);
            var result = await _mediator.Send(command);
            return result ? Ok("Player updated successfully.") : NotFound("Player not found.");
        }

        // -------------------------------
        // ❌ DELETE PLAYER (Admin)
        // -------------------------------
        [HttpDelete("DeletePlayerAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeletePlayerAsAdmin(Guid id)
        {
            var command = new AdminDeletePlayerCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok("Player deleted successfully.") : NotFound("Player not found.");
        }

        // -------------------------------
        // 🌱 SEED PLAYERS (Admin Utility)
        // -------------------------------
        [HttpPost("SeedPlayersAsAdmin")]
        public async Task<IActionResult> SeedPlayersAsAdmin([FromQuery] int count = 10)
        {
            if (count <= 0)
                return BadRequest("Count must be greater than zero.");

            await _mediator.Send(new RunPlayerSeedCommand(count));
            return Ok($"✅ {count} adet oyuncu başarıyla seedlendi.");
        }
    }
}
