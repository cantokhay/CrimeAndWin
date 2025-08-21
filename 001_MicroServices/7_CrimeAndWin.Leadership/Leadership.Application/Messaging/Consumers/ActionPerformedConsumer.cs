using Leadership.Application.Messaging.Concrete;
using Leadership.Domain.Entities;
using Leadership.Domain.VOs;
using Leadership.Infrastructure.Persistance.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Leadership.Application.Messaging.Consumers
{
    public class ActionPerformedConsumer : IConsumer<ActionPerformedEvent>
    {
        private readonly LeadershipDbContext _ctx;
        public async Task Consume(ConsumeContext<ActionPerformedEvent> context)
        {
            var msg = context.Message;


            var board = await _ctx.Leaderboards
            .FirstOrDefaultAsync(x => x.GameWorldId == msg.GameWorldId && x.SeasonId == msg.SeasonId);
            if (board is null)
            {
                board = new Leaderboard
                {
                    Id = Guid.NewGuid(),
                    Name = "Default",
                    Description = "Auto-created",
                    GameWorldId = msg.GameWorldId,
                    SeasonId = msg.SeasonId,
                    IsSeasonal = msg.SeasonId.HasValue,
                    CreatedAtUtc = DateTime.UtcNow
                };
                await _ctx.Leaderboards.AddAsync(board);
            }


            var entry = await _ctx.LeaderboardEntries
            .FirstOrDefaultAsync(x => x.LeaderboardId == board.Id && x.PlayerId == msg.PlayerId);


            if (entry is null)
            {
                entry = new LeaderboardEntry
                {
                    Id = Guid.NewGuid(),
                    LeaderboardId = board.Id,
                    PlayerId = msg.PlayerId,
                    Rank = new Rank { RankPoints = msg.RankPointsDelta, Position = 0 },
                    IsActive = true,
                    CreatedAtUtc = DateTime.UtcNow
                };
                await _ctx.LeaderboardEntries.AddAsync(entry);
            }
            else
            {
                var newPoints = entry.Rank.RankPoints + msg.RankPointsDelta;
                entry.Rank = entry.Rank with { RankPoints = newPoints };
                entry.UpdatedAtUtc = DateTime.UtcNow;
            }


            await _ctx.SaveChangesAsync();
        }
    }
}
