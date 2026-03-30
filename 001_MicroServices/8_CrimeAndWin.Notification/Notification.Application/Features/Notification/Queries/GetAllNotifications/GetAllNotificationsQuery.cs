using MediatR;
using Notification.Application.DTOs;

namespace Notification.Application.Features.Notification.Queries.GetAllNotifications
{
    public sealed record GetAllNotificationsQuery() : IRequest<List<ResultNotificationDTO>>;
}
