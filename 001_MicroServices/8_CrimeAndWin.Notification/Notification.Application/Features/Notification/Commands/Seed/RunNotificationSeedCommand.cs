﻿using MediatR;

namespace Notification.Application.Features.Notification.Commands.Seed
{
    public sealed record RunNotificationSeedCommand(int Count) : IRequest<Unit>;
}
