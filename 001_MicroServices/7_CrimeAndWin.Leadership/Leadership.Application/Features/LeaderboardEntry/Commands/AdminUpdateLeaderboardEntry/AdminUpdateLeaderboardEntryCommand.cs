using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminUpdateLeaderboardEntry
{
    public sealed record AdminUpdateLeaderboardEntryCommand(AdminUpdateLeaderboardEntryDTO updateLeaderboardEntryDTO) : IRequest<bool>;
}
