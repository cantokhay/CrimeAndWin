using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Inventory.Application.DTOs.ItemDTOs.Admin;
using Inventory.Application.Features.Inventory.Commands.AdminCreateInventory;
using Inventory.Application.Features.Inventory.Commands.AdminDeleteInventory;
using Inventory.Application.Features.Inventory.Commands.AdminUpdateInventory;
using Inventory.Application.Features.Inventory.Queries.GetAllInventoriesAsAdmin;
using Inventory.Application.Features.Inventory.Queries.GetInventoryByIdAsAdmin;
using Inventory.Application.Features.Item.Commands.AdminCreateItem;
using Inventory.Application.Features.Item.Commands.AdminDeleteItem;
using Inventory.Application.Features.Item.Commands.AdminUpdateItem;
using Inventory.Application.Features.Item.Queries.GetAllItemsAsAdmin;
using Inventory.Application.Features.Item.Queries.GetItemByIdAsAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        //          INVENTORY CRUD
        // ================================

        // GET: api/InventoryAdmins/GetAllInventoriesAsAdmin
        [HttpGet("GetAllInventoriesAsAdmin")]
        public async Task<IActionResult> GetAllInventoriesAsAdmin()
        {
            var result = await _mediator.Send(new GetAllInventoriesAsAdminQuery());
            return Ok(result);
        }

        // GET: api/InventoryAdmins/GetInventoryByIdAsAdmin/{id}
        [HttpGet("GetInventoryByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetInventoryByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetInventoryByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/InventoryAdmins/CreateInventoryAsAdmin
        [HttpPost("CreateInventoryAsAdmin")]
        public async Task<IActionResult> CreateInventoryAsAdmin([FromBody] AdminCreateInventoryDTO dto)
        {
            var command = new AdminCreateInventoryCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/InventoryAdmins/UpdateInventoryAsAdmin
        [HttpPut("UpdateInventoryAsAdmin")]
        public async Task<IActionResult> UpdateInventoryAsAdmin([FromBody] AdminUpdateInventoryDTO dto)
        {
            var command = new AdminUpdateInventoryCommand(dto);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Inventory updated successfully.") : NotFound("Inventory not found.");
        }

        // DELETE: api/InventoryAdmins/DeleteInventoryAsAdmin/{id}
        [HttpDelete("DeleteInventoryAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteInventoryAsAdmin(Guid id)
        {
            var command = new AdminDeleteInventoryCommand(id);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Inventory deleted successfully.") : NotFound("Inventory not found.");
        }

        // ================================
        //             ITEM CRUD
        // ================================

        // GET: api/InventoryAdmins/GetAllItemsAsAdmin
        [HttpGet("GetAllItemsAsAdmin")]
        public async Task<IActionResult> GetAllItemsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllItemsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/InventoryAdmins/GetItemByIdAsAdmin/{id}
        [HttpGet("GetItemByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetItemByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetItemByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/InventoryAdmins/CreateItemAsAdmin
        [HttpPost("CreateItemAsAdmin")]
        public async Task<IActionResult> CreateItemAsAdmin([FromBody] AdminCreateItemDTO dto)
        {
            var command = new AdminCreateItemCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/InventoryAdmins/UpdateItemAsAdmin
        [HttpPut("UpdateItemAsAdmin")]
        public async Task<IActionResult> UpdateItemAsAdmin([FromBody] AdminUpdateItemDTO dto)
        {
            var command = new AdminUpdateItemCommand(dto);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Item updated successfully.") : NotFound("Item not found.");
        }

        // DELETE: api/InventoryAdmins/DeleteItemAsAdmin/{id}
        [HttpDelete("DeleteItemAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteItemAsAdmin(Guid id)
        {
            var command = new AdminDeleteItemCommand(id);
            var ok = await _mediator.Send(command);
            return ok ? Ok("Item deleted successfully.") : NotFound("Item not found.");
        }
    }
}
