using Mediator;

namespace Notification.Application.Features.Notification.Commands.Seed
{
    public sealed record RunNotificationSeedCommand(int count) : IRequest<Unit>;
}

