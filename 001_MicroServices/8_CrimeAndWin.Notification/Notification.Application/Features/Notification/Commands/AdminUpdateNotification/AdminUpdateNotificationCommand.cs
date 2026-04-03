using Shared.Application.Abstractions.Messaging;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.Features.Notification.Commands.AdminUpdateNotification
{
    public sealed record AdminUpdateNotificationCommand(AdminUpdateNotificationDTO updateNotificationDTO) : IRequest<bool>;
}


