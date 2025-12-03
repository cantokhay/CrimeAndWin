using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Queries.GetAllLeaderboardsAsAdmin
{
    public sealed class GetAllLeaderboardsAsAdminQueryHandler
            : IRequestHandler<GetAllLeaderboardsAsAdminQuery, List<AdminResultLeaderboardDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Leaderboard> _read;

        public GetAllLeaderboardsAsAdminQueryHandler(IReadRepository<Domain.Entities.Leaderboard> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultLeaderboardDTO>> Handle(GetAllLeaderboardsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(lb => new AdminResultLeaderboardDTO
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
                })
                .ToListAsync(cancellationToken);
        }
    }
}
