using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Leadership.Application.Features.Leaderboard.Commands.AdminCreateLeaderboard;
using Leadership.Application.Features.Leaderboard.Commands.AdminDeleteLeaderboard;
using Leadership.Application.Features.Leaderboard.Commands.AdminUpdateLeaderboard;
using Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboardsAsAdmin;
using Leadership.Application.Features.Leaderboard.Queries.GetLeaderboardByIdAsAdmin;
using Leadership.Application.Features.LeaderboardEntry.Commands.AdminCreateLeaderboardEntry;
using Leadership.Application.Features.LeaderboardEntry.Commands.AdminDeleteLeaderboardEntry;
using Leadership.Application.Features.LeaderboardEntry.Commands.AdminUpdateLeaderboardEntry;
using Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntriesAsAdmin;
using Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByIdAsAdmin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leadership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadershipAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeadershipAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        //        LEADERBOARD CRUD
        // ================================

        [HttpGet("GetAllLeaderboardsAsAdmin")]
        public async Task<IActionResult> GetAllLeaderboardsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllLeaderboardsAsAdminQuery());
            return Ok(result);
        }

        [HttpGet("GetLeaderboardByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetLeaderboardByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetLeaderboardByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("CreateLeaderboardAsAdmin")]
        public async Task<IActionResult> CreateLeaderboardAsAdmin([FromBody] AdminCreateLeaderboardDTO dto)
        {
            var id = await _mediator.Send(new AdminCreateLeaderboardCommand(dto));
            return Ok(id);
        }

        [HttpPut("UpdateLeaderboardAsAdmin")]
        public async Task<IActionResult> UpdateLeaderboardAsAdmin([FromBody] AdminUpdateLeaderboardDTO dto)
        {
            var ok = await _mediator.Send(new AdminUpdateLeaderboardCommand(dto));
            return ok ? Ok("Leaderboard updated successfully.") : NotFound("Leaderboard not found.");
        }

        [HttpDelete("DeleteLeaderboardAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteLeaderboardAsAdmin(Guid id)
        {
            var ok = await _mediator.Send(new AdminDeleteLeaderboardCommand(id));
            return ok ? Ok("Leaderboard deleted successfully.") : NotFound("Leaderboard not found.");
        }

        // ================================
        //     LEADERBOARD ENTRY CRUD
        // ================================

        [HttpGet("GetAllLeaderboardEntriesAsAdmin")]
        public async Task<IActionResult> GetAllLeaderboardEntriesAsAdmin()
        {
            var result = await _mediator.Send(new GetAllLeaderboardEntriesAsAdminQuery());
            return Ok(result);
        }

        [HttpGet("GetLeaderboardEntryByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetLeaderboardEntryByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetLeaderboardEntryByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("CreateLeaderboardEntryAsAdmin")]
        public async Task<IActionResult> CreateLeaderboardEntryAsAdmin([FromBody] AdminCreateLeaderboardEntryDTO dto)
        {
            var id = await _mediator.Send(new AdminCreateLeaderboardEntryCommand(dto));
            return Ok(id);
        }

        [HttpPut("UpdateLeaderboardEntryAsAdmin")]
        public async Task<IActionResult> UpdateLeaderboardEntryAsAdmin([FromBody] AdminUpdateLeaderboardEntryDTO dto)
        {
            var ok = await _mediator.Send(new AdminUpdateLeaderboardEntryCommand(dto));
            return ok ? Ok("LeaderboardEntry updated successfully.") : NotFound("LeaderboardEntry not found.");
        }

        [HttpDelete("DeleteLeaderboardEntryAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteLeaderboardEntryAsAdmin(Guid id)
        {
            var ok = await _mediator.Send(new AdminDeleteLeaderboardEntryCommand(id));
            return ok ? Ok("LeaderboardEntry deleted successfully.") : NotFound("LeaderboardEntry not found.");
        }
    }
}
