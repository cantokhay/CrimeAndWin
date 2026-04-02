using Mediator;

namespace Moderation.Application.Features.Report.Commands.Seed
{
    public sealed record RunModerationSeedCommand(int Count) : IRequest<Unit>;

}

