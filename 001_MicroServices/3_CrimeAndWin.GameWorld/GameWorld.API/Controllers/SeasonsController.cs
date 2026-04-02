using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Application.Features.Season.Commands.CreateSeason;
using GameWorld.Application.Features.Season.Commands.DeleteSeason;
using GameWorld.Application.Features.Season.Commands.UpdateSeason;
using GameWorld.Application.Features.Season.Queries.GetAllSeason;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Season.Application.Features.Season.Queries;

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
            if (id != body.GameWorldId) return BadRequest("Route id ile body id eþleþmiyor.");
            return Ok(await _mediator.Send(body));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultSeasonDTO>> GetById(Guid id)
        => Ok(await _mediator.Send(new GetSeasonByIdQuery(id)));

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultSeasonDTO>>> GetAll()
            => Ok(await _mediator.Send(new GetAllSeasonsQuery()));

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateSeasonDTO>> Update(Guid id, [FromBody] UpdateSeasonCommand cmd)
        {
            if (id != cmd.SeasonId)
                return BadRequest("Route id ile body id eþleþmiyor.");

            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        // -------------------------------
        // ? DELETE SEASON (Admin)
        // -------------------------------
        [HttpDelete("DeleteSeason/{id:guid}")]
        public async Task<IActionResult> DeletePlayerAsAdmin(Guid id)
        {
            var command = new DeleteSeasonCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok("Season deleted successfully.") : NotFound("Season not found.");
        }
    }
}


