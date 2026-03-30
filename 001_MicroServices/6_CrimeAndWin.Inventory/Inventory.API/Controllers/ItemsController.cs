using AutoMapper;
using Inventory.Application.DTOs.ItemDTOs;
using Inventory.Application.Features.Item.Commands;
using Inventory.Application.Features.Item.Commands.AddItem;
using Inventory.Application.Features.Item.Commands.GetAllItems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{inventoryId:guid}/items")]
        public async Task<IActionResult> AddItem(Guid inventoryId, [FromBody] ResultItemDTO resultItemDTO)
        {
            // DTO validation FluentValidation ile otomatik
            var cmd = new AddItemCommand(inventoryId, resultItemDTO.Name, resultItemDTO.Quantity,
                resultItemDTO.Stats.Damage, resultItemDTO.Stats.Defense, resultItemDTO.Stats.Power,
                resultItemDTO.Value.Amount, resultItemDTO.Value.Currency);

            var itemId = await _mediator.Send(cmd);
            return Ok(itemId);
        }

        /// <summary>
        /// Tüm Item kayıtlarını getirir.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultItemDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllItemsQuery());
            return Ok(result);
        }
    }
}
