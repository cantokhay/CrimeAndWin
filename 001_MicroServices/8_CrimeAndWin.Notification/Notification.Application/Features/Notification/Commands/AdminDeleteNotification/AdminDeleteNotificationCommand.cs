using MediatR;

namespace Notification.Application.Features.Notification.Commands.AdminDeleteNotification
{
    public sealed record AdminDeleteNotificationCommand(Guid id) : IRequest<bool>;
}
