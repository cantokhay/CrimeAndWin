using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Queries.GetLeaderboardEntryByIdAsAdmin
{
    public sealed class GetLeaderboardEntryByIdAsAdminQueryHandler
            : IRequestHandler<GetLeaderboardEntryByIdAsAdminQuery, AdminResultLeaderboardEntryDTO?>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _read;

        public GetLeaderboardEntryByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.LeaderboardEntry> read)
        {
            _read = read;
        }

        public async Task<AdminResultLeaderboardEntryDTO?> Handle(GetLeaderboardEntryByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var e = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (e is null) return null;

            return new AdminResultLeaderboardEntryDTO
            {
                Id = e.Id,
                LeaderboardId = e.LeaderboardId,
                PlayerId = e.PlayerId,
                RankPoints = e.Rank.RankPoints,
                Position = e.Rank.Position,
                IsActive = e.IsActive,
                CreatedAtUtc = e.CreatedAtUtc,
                UpdatedAtUtc = e.UpdatedAtUtc
            };
        }
    }
}
