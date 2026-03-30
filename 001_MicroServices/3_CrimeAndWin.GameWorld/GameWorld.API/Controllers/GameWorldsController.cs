using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld;
using GameWorld.Application.Features.GameWorld.Commands.DeleteGameWorld;
using GameWorld.Application.Features.GameWorld.Commands.Seed;
using GameWorld.Application.Features.GameWorld.Commands.UpdateGameWorld;
using GameWorld.Application.Features.GameWorld.Queries.GetByIdGameWorld;
using GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameWorld.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameWorldsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GameWorldsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateGameWorldDTO>> Create([FromBody] CreateGameWorldCommand cmd)
          => Ok(await _mediator.Send(cmd));

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultGameWorldDTO>> GetById(Guid id)
         => Ok(await _mediator.Send(new GetGameWorldByIdQuery(id)));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ResultGameWorldDTO>>> List()
         => Ok(await _mediator.Send(new GetGameWorldListQuery()));

        [HttpPut("{id}/GameWorld")]
        public async Task<ActionResult<UpdateGameWorldDTO>> UpdateGameWorld(Guid id, [FromBody] UpdateGameWorldCommand body)
        {
            if (id != body.GameWorldId) return BadRequest("Route id ile body id eşleşmiyor.");
            return Ok(await _mediator.Send(body));
        }

        // -------------------------------
        // ❌ DELETE GAME WORLD (Admin)
        // -------------------------------
        [HttpDelete("DeleteGameWorld/{id:guid}")]
        public async Task<IActionResult> DeletePlayerAsAdmin(Guid id)
        {
            var command = new DeleteGameWorldCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok("Game World deleted successfully.") : NotFound("Game World not found.");
        }

        /// <summary>
        /// Bogus ile rastgele GameWorld + Season verileri oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 3)
        {
            await _mediator.Send(new RunGameWorldSeedCommand(count));
            return Ok(new { message = $"{count} adet GameWorld başarıyla seed edildi." });
        }
    }
}
