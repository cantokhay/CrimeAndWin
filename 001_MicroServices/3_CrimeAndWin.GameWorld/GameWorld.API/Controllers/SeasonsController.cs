using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Application.Features.Season.Commands.CreateSeason;
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
    }
}
