using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard;
using Leadership.Application.Features.Leaderboard.Commands.Seed;
using Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboards;
using Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leadership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public LeaderboardsController(IMediator mediator, IMapper mapper)
        { _mediator = mediator; _mapper = mapper; }


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
        /// Tüm Leaderboard kayıtlarını ve Entries detaylarını listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultLeaderboardDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllLeaderboardsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele Leaderboard ve LeaderboardEntry verileri oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunLeadershipSeedCommand(count));
            return Ok(new { message = $"{count} adet Leaderboard başarıyla seed edildi." });
        }
    }
}
