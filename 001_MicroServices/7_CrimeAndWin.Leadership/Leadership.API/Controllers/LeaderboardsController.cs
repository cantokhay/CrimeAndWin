using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.Features.Leaderboard.Commands;
using Leadership.Application.Features.Leaderboard.Queries;
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
    }
}
