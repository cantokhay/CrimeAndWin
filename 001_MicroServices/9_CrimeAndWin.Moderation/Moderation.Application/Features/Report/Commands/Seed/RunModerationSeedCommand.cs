using Shared.Application.Abstractions.Messaging;

namespace Moderation.Application.Features.Report.Commands.Seed
{
    public sealed record RunModerationSeedCommand(int Count) : IRequest<Unit>;

}


