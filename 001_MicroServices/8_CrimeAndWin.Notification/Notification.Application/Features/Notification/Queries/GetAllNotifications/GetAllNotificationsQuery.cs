using Shared.Application.Abstractions.Messaging;
using Notification.Application.DTOs;

namespace Notification.Application.Features.Notification.Queries.GetAllNotifications
{
    public sealed record GetAllNotificationsQuery() : IRequest<List<ResultNotificationDTO>>;
}


