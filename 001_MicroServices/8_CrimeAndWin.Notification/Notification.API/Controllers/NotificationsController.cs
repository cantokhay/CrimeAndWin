using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.DTOs;
using Notification.Application.Features.Notification.Commands.CreateNotification;
using Notification.Application.Features.Notification.Commands.Seed;
using Notification.Application.Features.Notification.Queries.GetAllNotifications;
using Notification.Application.Features.Notification.Queries.GetNotificationByPlayerId;

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
        public async Task<IActionResult> GetByPlayer(Guid playerId)
        {
            var notifications = await _mediator.Send(new GetNotificationByPlayerIdQuery(playerId));
            return Ok(notifications);
        }

        /// <summary>
        /// Tüm Notification kayıtlarını listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultNotificationDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllNotificationsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele Notification verilerini oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunNotificationSeedCommand(count));
            return Ok(new { message = $"{count} oyuncu için rastgele bildirimler oluşturuldu." });
        }
    }
}
