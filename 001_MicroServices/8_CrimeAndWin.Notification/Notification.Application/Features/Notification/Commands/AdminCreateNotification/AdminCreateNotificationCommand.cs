using MediatR;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.Features.Notification.Commands.AdminCreateNotification
{
    public sealed record AdminCreateNotificationCommand(AdminCreateNotificationDTO createNotificationDTO) : IRequest<Guid>;
}
