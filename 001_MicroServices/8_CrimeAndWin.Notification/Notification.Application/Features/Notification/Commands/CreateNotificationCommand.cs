using MediatR;

namespace Notification.Application.Features.Notification.Commands
{
    public record CreateNotificationCommand(Guid PlayerId, string Title, string Message, string Type)
        : IRequest<Guid>;
}
