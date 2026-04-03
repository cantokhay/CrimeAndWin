using Shared.Application.Abstractions.Messaging;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.Features.Notification.Commands.AdminCreateNotification
{
    public sealed record AdminCreateNotificationCommand(AdminCreateNotificationDTO createNotificationDTO) : IRequest<Guid>;
}


