using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using Inventory.Application.Features.Inventory.Commands;
using Inventory.Application.Features.Inventory.Queries;
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
    }
}
