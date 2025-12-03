using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminDeleteLeaderboardEntry
{
    public sealed record AdminDeleteLeaderboardEntryCommand(Guid id) : IRequest<bool>;
}
