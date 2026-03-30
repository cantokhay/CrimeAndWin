using MediatR;
using Notification.Application.DTOs;

namespace Notification.Application.Features.Notification.Queries.GetNotificationByPlayerId
{
    public record GetNotificationByPlayerIdQuery(Guid PlayerId) : IRequest<List<ResultNotificationDTO>>;
}
