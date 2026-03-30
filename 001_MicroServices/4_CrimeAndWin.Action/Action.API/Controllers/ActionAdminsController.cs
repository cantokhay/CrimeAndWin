using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Action.Application.Features.ActionDefinitons.Commands.AdminCreateAction;
using Action.Application.Features.ActionDefinitons.Commands.AdminDeleteAction;
using Action.Application.Features.ActionDefinitons.Commands.AdminUpdateAction;
using Action.Application.Features.ActionDefinitons.Queries.GetActionDefinitionByIdAsAdmin;
using Action.Application.Features.ActionDefinitons.Queries.GetAllActionDefinitionAsAdmin;
using Action.Application.Features.PlayerActionAttempts.Commands.AdminCreatePlayerActionAttempt;
using Action.Application.Features.PlayerActionAttempts.Commands.AdminDeletePlayerActionAttempt;
using Action.Application.Features.PlayerActionAttempts.Commands.AdminUpdatePlayerActionAttempt;
using Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerActionAttemptsAsAdmin;
using Action.Application.Features.PlayerActionAttempts.Queries.GetPlayerActionAttemptByIdAsAdmin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Action.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActionAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        //      ACTION DEFINITION CRUD
        // ================================

        // GET: api/ActionAdmins/GetAllActionDefinitionsAsAdmin
        [HttpGet("GetAllActionDefinitionsAsAdmin")]
        public async Task<IActionResult> GetAllActionDefinitionsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllActionDefinitionsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/ActionAdmins/GetActionDefinitionByIdAsAdmin/{id}
        [HttpGet("GetActionDefinitionByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetActionDefinitionByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetActionDefinitionByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/ActionAdmins/CreateActionDefinitionAsAdmin
        [HttpPost("CreateActionDefinitionAsAdmin")]
        public async Task<IActionResult> CreateActionDefinitionAsAdmin([FromBody] AdminCreateActionDefinitionDTO dto)
        {
            var command = new AdminCreateActionDefinitionCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/ActionAdmins/UpdateActionDefinitionAsAdmin
        [HttpPut("UpdateActionDefinitionAsAdmin")]
        public async Task<IActionResult> UpdateActionDefinitionAsAdmin([FromBody] AdminUpdateActionDefinitionDTO dto)
        {
            var command = new AdminUpdateActionDefinitionCommand(dto);
            var result = await _mediator.Send(command);
            return result ? Ok("ActionDefinition updated successfully.") : NotFound("ActionDefinition not found.");
        }

        // DELETE: api/ActionAdmins/DeleteActionDefinitionAsAdmin/{id}
        [HttpDelete("DeleteActionDefinitionAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteActionDefinitionAsAdmin(Guid id)
        {
            var command = new AdminDeleteActionDefinitionCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok("ActionDefinition deleted successfully.") : NotFound("ActionDefinition not found.");
        }

        // ================================
        //   PLAYER ACTION ATTEMPT CRUD
        // ================================

        // GET: api/ActionAdmins/GetAllPlayerActionAttemptsAsAdmin
        [HttpGet("GetAllPlayerActionAttemptsAsAdmin")]
        public async Task<IActionResult> GetAllPlayerActionAttemptsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllPlayerActionAttemptsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/ActionAdmins/GetPlayerActionAttemptByIdAsAdmin/{id}
        [HttpGet("GetPlayerActionAttemptByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetPlayerActionAttemptByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetPlayerActionAttemptByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/ActionAdmins/CreatePlayerActionAttemptAsAdmin
        [HttpPost("CreatePlayerActionAttemptAsAdmin")]
        public async Task<IActionResult> CreatePlayerActionAttemptAsAdmin([FromBody] AdminCreatePlayerActionAttemptDTO dto)
        {
            var command = new AdminCreatePlayerActionAttemptCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/ActionAdmins/UpdatePlayerActionAttemptAsAdmin
        [HttpPut("UpdatePlayerActionAttemptAsAdmin")]
        public async Task<IActionResult> UpdatePlayerActionAttemptAsAdmin([FromBody] AdminUpdatePlayerActionAttemptDTO dto)
        {
            var command = new AdminUpdatePlayerActionAttemptCommand(dto);
            var result = await _mediator.Send(command);
            return result ? Ok("PlayerActionAttempt updated successfully.") : NotFound("PlayerActionAttempt not found.");
        }

        // DELETE: api/ActionAdmins/DeletePlayerActionAttemptAsAdmin/{id}
        [HttpDelete("DeletePlayerActionAttemptAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeletePlayerActionAttemptAsAdmin(Guid id)
        {
            var command = new AdminDeletePlayerActionAttemptCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok("PlayerActionAttempt deleted successfully.") : NotFound("PlayerActionAttempt not found.");
        }
    }
}
