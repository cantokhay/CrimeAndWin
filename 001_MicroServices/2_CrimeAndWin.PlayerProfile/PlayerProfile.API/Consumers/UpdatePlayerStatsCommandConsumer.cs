using MassTransit;
using Shared.Domain.Repository;
using PlayerProfile.Domain.Entities;
using CrimeAndWin.Contracts.Commands.PlayerProfile;
using CrimeAndWin.Contracts.Events.PlayerProfile;
using PlayerProfile.Domain.VOs;

namespace PlayerProfile.API.Consumers
{
    public class UpdatePlayerStatsCommandConsumer : IConsumer<UpdatePlayerStatsCommand>
    {
        private readonly IWriteRepository<Player> _playerWrite;
        private readonly IReadRepository<Player> _playerRead;

        public UpdatePlayerStatsCommandConsumer(IWriteRepository<Player> playerWrite, IReadRepository<Player> playerRead)
        {
            _playerWrite = playerWrite;
            _playerRead = playerRead;
        }

        public async Task Consume(ConsumeContext<UpdatePlayerStatsCommand> context)
        {
            var msg = context.Message;
            try
            {
                var player = _playerRead.GetWhere(p => p.Id == msg.PlayerId || p.AppUserId == msg.PlayerId).FirstOrDefault();
                if (player == null) throw new Exception("Player profile not found.");

                int newEnergy = player.Energy.Current + msg.EnergyDelta;
                if (msg.EnergyDelta < 0 && newEnergy < 0) throw new Exception("Not enough energy to perform this action.");
                if (newEnergy > player.Energy.Max) newEnergy = player.Energy.Max;

                int newExp = player.Rank.RankPoints + msg.ExpDelta;
                if (newExp < 0) newExp = 0;

                player.Energy = new Energy(newEnergy, player.Energy.Max, player.Energy.RegenPerMinute);
                player.Rank = new Rank(newExp, player.Rank.Position);

                _playerWrite.Update(player);
                var saved = await _playerWrite.SaveAsync();

                await context.Publish(new PlayerStatsUpdatedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = saved > 0,
                    FailReason = saved > 0 ? null : "Could not save player stats."
                });
            }
            catch (Exception ex)
            {
                await context.Publish(new PlayerStatsUpdatedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = false,
                    FailReason = ex.Message
                });
            }
        }
    }
}
