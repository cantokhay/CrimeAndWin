using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Application.Features.Season.Commands.CreateSeason;
using GameWorld.Application.Features.Season.Commands.UpdateSeason;
using GameWorld.Application.Features.Season.Queries.GetAllSeason;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SeasonsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("{id}/seasons")]
        public async Task<ActionResult<CreateSeasonDTO>> CreateSeason(Guid id, [FromBody] CreateSeasonCommand body)
        {
            if (id != body.GameWorldId) return BadRequest("Route id ile body id eşleşmiyor.");
            return Ok(await _mediator.Send(body));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultSeasonDTO>>> GetAll()
            => Ok(await _mediator.Send(new GetAllSeasonsQuery()));

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateSeasonDTO>> Update(Guid id, [FromBody] UpdateSeasonCommand cmd)
        {
            if (id != cmd.SeasonId)
                return BadRequest("Route id ile body id eşleşmiyor.");

            var result = await _mediator.Send(cmd);
            return Ok(result);
        }
    }
}
