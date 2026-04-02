using Mediator;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminDeleteLeaderboardEntry
{
    public sealed record AdminDeleteLeaderboardEntryCommand(Guid id) : IRequest<bool>;
}

