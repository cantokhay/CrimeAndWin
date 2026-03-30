using MediatR;

namespace Notification.Application.Features.Notification.Commands.CreateNotification
{
    public record CreateNotificationCommand(Guid PlayerId, string Title, string Message, string Type)
        : IRequest<Guid>;
}
