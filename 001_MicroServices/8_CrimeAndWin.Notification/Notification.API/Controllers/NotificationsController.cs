using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Features.Notification.Commands;
using Notification.Application.Features.Notification.Queries;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet("{playerId:guid}")]
        public async Task<IActionResult> Get(Guid playerId)
        {
            var notifications = await _mediator.Send(new GetNotificationQuery(playerId));
            return Ok(notifications);
        }
    }
}
