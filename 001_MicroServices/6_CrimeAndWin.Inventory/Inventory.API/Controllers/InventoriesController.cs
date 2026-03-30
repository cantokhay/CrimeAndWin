using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using Inventory.Application.Features.Inventory.Commands.CreateInventory;
using Inventory.Application.Features.Inventory.Commands.Seed;
using Inventory.Application.Features.Inventory.Queries.GetAllInventories;
using Inventory.Application.Features.Inventory.Queries.GetInventoryByPlayerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InventoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] Guid playerId)
        {
            var id = await _mediator.Send(new CreateInventoryCommand(playerId));
            return CreatedAtAction(nameof(GetInventoryByPlayerId), new { playerId }, id);
        }

        [HttpGet("{playerId:guid}")]
        public async Task<ActionResult<ResultInventoryDTO>> GetInventoryByPlayerId(Guid playerId)
        {
            var dto = await _mediator.Send(new GetInventoryByPlayerIdQuery(playerId));
            return dto is null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Rastgele Inventory + Item verileri oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunInventorySeedCommand(count));
            return Ok(new { message = $"{count} adet Inventory başarıyla seed edildi." });
        }

        /// <summary>
        /// Tüm Inventory kayıtlarını ve içindeki Item'ları döndürür.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultInventoryDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllInventoriesQuery());
            return Ok(result);
        }
    }
}
