using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Commands.Seed
{
    public sealed record RunActionSeedCommand(int Count) : IRequest<Unit>;
}


