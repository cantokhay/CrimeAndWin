using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetAllLeaderboardEntriesAsAdmin
{
    public sealed class GetAllLeaderboardEntriesAsAdminQueryHandler
            : IRequestHandler<GetAllLeaderboardEntriesAsAdminQuery, List<AdminResultLeaderboardEntryDTO>>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _read;

        public GetAllLeaderboardEntriesAsAdminQueryHandler(IReadRepository<Domain.Entities.LeaderboardEntry> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultLeaderboardEntryDTO>> Handle(GetAllLeaderboardEntriesAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(e => new AdminResultLeaderboardEntryDTO
                {
                    Id = e.Id,
                    LeaderboardId = e.LeaderboardId,
                    PlayerId = e.PlayerId,
                    RankPoints = e.Rank.RankPoints,
                    Position = e.Rank.Position,
                    IsActive = e.IsActive,
                    CreatedAtUtc = e.CreatedAtUtc,
                    UpdatedAtUtc = e.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
