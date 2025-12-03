using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.DTOs.Admin;
using Notification.Application.Features.Notification.Commands.AdminCreateNotification;
using Notification.Application.Features.Notification.Commands.AdminDeleteNotification;
using Notification.Application.Features.Notification.Commands.AdminUpdateNotification;
using Notification.Application.Features.Notification.Queries.GetAllNotificationsAsAdmin;
using Notification.Application.Features.Notification.Queries.GetNotificationByIdAsAdmin;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/NotificationAdmins/GetAllNotificationsAsAdmin
        [HttpGet("GetAllNotificationsAsAdmin")]
        public async Task<IActionResult> GetAllNotificationsAsAdmin()
        {
            var result = await _mediator.Send(new GetAllNotificationsAsAdminQuery());
            return Ok(result);
        }

        // GET: api/NotificationAdmins/GetNotificationByIdAsAdmin/{id}
        [HttpGet("GetNotificationByIdAsAdmin/{id:guid}")]
        public async Task<IActionResult> GetNotificationByIdAsAdmin(Guid id)
        {
            var result = await _mediator.Send(new GetNotificationByIdAsAdminQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/NotificationAdmins/CreateNotificationAsAdmin
        [HttpPost("CreateNotificationAsAdmin")]
        public async Task<IActionResult> CreateNotificationAsAdmin([FromBody] AdminCreateNotificationDTO dto)
        {
            var id = await _mediator.Send(new AdminCreateNotificationCommand(dto));
            return Ok(id);
        }

        // PUT: api/NotificationAdmins/UpdateNotificationAsAdmin
        [HttpPut("UpdateNotificationAsAdmin")]
        public async Task<IActionResult> UpdateNotificationAsAdmin([FromBody] AdminUpdateNotificationDTO dto)
        {
            var ok = await _mediator.Send(new AdminUpdateNotificationCommand(dto));
            return ok ? Ok("Notification updated successfully.") : NotFound("Notification not found.");
        }

        // DELETE: api/NotificationAdmins/DeleteNotificationAsAdmin/{id}
        [HttpDelete("DeleteNotificationAsAdmin/{id:guid}")]
        public async Task<IActionResult> DeleteNotificationAsAdmin(Guid id)
        {
            var ok = await _mediator.Send(new AdminDeleteNotificationCommand(id));
            return ok ? Ok("Notification deleted successfully.") : NotFound("Notification not found.");
        }
    }
}
