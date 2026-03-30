using Leadership.Application.Messaging.Concrete;
using Leadership.Infrastructure.Persistance.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Leadership.Application.Messaging.Consumers
{
    public class PlayerBannedConsumer : IConsumer<PlayerBannedEvent>
    {
        private readonly LeadershipDbContext _ctx;
        public PlayerBannedConsumer(LeadershipDbContext ctx) => _ctx = ctx;


        public async Task Consume(ConsumeContext<PlayerBannedEvent> context)
        {
            var msg = context.Message;
            var entries = await _ctx.LeaderboardEntries.Where(x => x.PlayerId == msg.PlayerId).ToListAsync();
            foreach (var e in entries)
            {
                e.IsActive = false;
                e.UpdatedAtUtc = DateTime.UtcNow;
            }
            await _ctx.SaveChangesAsync();
        }
    }
}
