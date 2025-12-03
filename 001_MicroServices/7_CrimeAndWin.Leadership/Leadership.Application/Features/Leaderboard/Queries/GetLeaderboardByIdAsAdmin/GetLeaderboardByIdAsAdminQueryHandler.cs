using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries.GetLeaderboardByIdAsAdmin
{
    public sealed class GetLeaderboardByIdAsAdminQueryHandler
            : IRequestHandler<GetLeaderboardByIdAsAdminQuery, AdminResultLeaderboardDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Leaderboard> _read;

        public GetLeaderboardByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Leaderboard> read)
        {
            _read = read;
        }

        public async Task<AdminResultLeaderboardDTO?> Handle(GetLeaderboardByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var lb = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (lb is null) return null;

            return new AdminResultLeaderboardDTO
            {
                Id = lb.Id,
                Name = lb.Name,
                Description = lb.Description,
                GameWorldId = lb.GameWorldId,
                SeasonId = lb.SeasonId,
                IsSeasonal = lb.IsSeasonal,
                CreatedAtUtc = lb.CreatedAtUtc,
                UpdatedAtUtc = lb.UpdatedAtUtc,
                Entries = lb.Entries.Select(e => new AdminResultLeaderboardEntryDTO
                {
                    Id = e.Id,
                    LeaderboardId = e.LeaderboardId,
                    PlayerId = e.PlayerId,
                    RankPoints = e.Rank.RankPoints,
                    Position = e.Rank.Position,
                    IsActive = e.IsActive,
                    CreatedAtUtc = e.CreatedAtUtc,
                    UpdatedAtUtc = e.UpdatedAtUtc
                }).ToList()
            };
        }
    }
}
