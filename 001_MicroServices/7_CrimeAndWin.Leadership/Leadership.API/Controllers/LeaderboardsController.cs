using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard;
using Leadership.Application.Features.Leaderboard.Commands.Seed;
using Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards;
using Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById;
using Shared.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Leadership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaderboardsController(IMediator mediator)
        { 
            _mediator = mediator; 
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeaderboardDTO dto)
        {
            var id = await _mediator.Send(new CreateLeaderboardCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLeaderboardByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Tüm Leaderboard kayýtlarýný ve Entries detaylarýný listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultLeaderboardDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllLeaderboardsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele Leaderboard ve LeaderboardEntry verileri oluþturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunLeadershipSeedCommand(count));
            return Ok(new { message = $"{count} adet Leaderboard baþarýyla seed edildi." });
        }
    }
}


