using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;
using Moderation.Application.DTOs.ReportDTOs.Admin;
using Moderation.Application.Features.ModerationAction.Commands.AdminCreateModerationAction;
using Moderation.Application.Features.ModerationAction.Commands.AdminDeleteModerationAction;
using Moderation.Application.Features.ModerationAction.Commands.AdminUpdateModerationAction;
using Moderation.Application.Features.ModerationAction.Queries.GetAllModerationActionsAsAdmin;
using Moderation.Application.Features.ModerationAction.Queries.GetModerationActionByIdAsAdmin;
using Moderation.Application.Features.Report.Commands.AdminCreateReport;
using Moderation.Application.Features.Report.Commands.AdminDeleteReport;
using Moderation.Application.Features.Report.Commands.AdminUpdateReport;
using Moderation.Application.Features.Report.Queries.GetAllReportsAsAdmin;
using Moderation.Application.Features.Report.Queries.GetReportByIdAsAdmin;

namespace Moderation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModerationAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModerationAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ================================
        //           REPORT CRUD
        // ================================

        [HttpGet("GetAllReportsAsAdmin")]
        public async Task<IActionResult> GetAllReportsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllReportsAsAdminQuery());
            return Ok(result);
        }

        [HttpGet("GetReportByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetReportByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetReportByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("CreateReportAsAdmin")]
        public async Task<IActionResult> CreateReportAsAdmin([FromBody] AdminCreateReportDTO dto)
        {
            var id = await _mediator.Send(new AdminCreateReportCommand(dto));
            return Ok(id);
        }

        [HttpPut("UpdateReportAsAdmin")]
        public async Task<IActionResult> UpdateReportAsAdmin([FromBody] AdminUpdateReportDTO dto)
        {
            var ok = await _mediator.Send(new AdminUpdateReportCommand(dto));
            return ok ? Ok("Report updated successfully.") : NotFound("Report not found.");
        }

        [HttpDelete("DeleteReportAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteReportAsAdmin(Guid id)
        {
            var ok = await _mediator.Send(new AdminDeleteReportCommand(id));
            return ok ? Ok("Report deleted successfully.") : NotFound("Report not found.");
        }

        // ================================
        //      MODERATION ACTION CRUD
        // ================================

        [HttpGet("GetAllModerationActionsAsAdmin")]
        public async Task<IActionResult> GetAllModerationActionsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllModerationActionsAsAdminQuery());
            return Ok(result);
        }

        [HttpGet("GetModerationActionByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetModerationActionByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetModerationActionByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("CreateModerationActionAsAdmin")]
        public async Task<IActionResult> CreateModerationActionAsAdmin([FromBody] AdminCreateModerationActionDTO dto)
        {
            var id = await _mediator.Send(new AdminCreateModerationActionCommand(dto));
            return Ok(id);
        }

        [HttpPut("UpdateModerationActionAsAdmin")]
        public async Task<IActionResult> UpdateModerationActionAsAdmin([FromBody] AdminUpdateModerationActionDTO dto)
        {
            var ok = await _mediator.Send(new AdminUpdateModerationActionCommand(dto));
            return ok ? Ok("ModerationAction updated successfully.") : NotFound("ModerationAction not found.");
        }

        [HttpDelete("DeleteModerationActionAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteModerationActionAsAdmin(Guid id)
        {
            var ok = await _mediator.Send(new AdminDeleteModerationActionCommand(id));
            return ok ? Ok("ModerationAction deleted successfully.") : NotFound("ModerationAction not found.");
        }
    }
}
