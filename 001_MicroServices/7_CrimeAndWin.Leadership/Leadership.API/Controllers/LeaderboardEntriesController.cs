using AutoMapper;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Leadership.Application.Features.LeaderboardEntry.Commands;
using Leadership.Application.Features.LeaderboardEntry.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leadership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardEntriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LeaderboardEntriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}/entries")]
        public async Task<IActionResult> GetEntries(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var list = await _mediator.Send(new GetEntriesByLeaderboardIdQuery(id, page, pageSize));
            return Ok(list);
        }


        [HttpPut("{id:guid}/entries")]
        public async Task<IActionResult> CreateLeadeboardEntry(Guid id, [FromBody] CreateLeaderboardEntryDTO dto)
        {
            var entryId = await _mediator.Send(new CreateLeaderboardEntryCommand(id, dto));
            return Ok(new { id = entryId });
        }


        [HttpGet("{id:guid}/entries/players/{playerId:guid}")]
        public async Task<IActionResult> GetPlayerRank(Guid id, Guid playerId)
        {
            var result = await _mediator.Send(new GetPlayerRankQuery(id, playerId));
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
