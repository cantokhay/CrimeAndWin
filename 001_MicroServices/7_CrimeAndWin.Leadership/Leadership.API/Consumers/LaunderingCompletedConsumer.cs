using MassTransit;
using CrimeAndWin.Contracts.Events.Economy;
using CrimeAndWin.Contracts.Commands.Economy;

namespace Leadership.API.Consumers
{
    public class LaunderingCompletedConsumer : IConsumer<LaunderingCompletedEvent>
    {
        private const decimal TaxRate = 0.05m; // %5 District Tax

        public async Task Consume(ConsumeContext<LaunderingCompletedEvent> context)
        {
            var msg = context.Message;
            
            // In a real scenario:
            // 1. Fetch district leader for the area where player lauandred money
            // var districtLeader = await _leadershipService.GetLeaderForDistrictAsync(msg.DistrictId);
            
            // MVP: Mocking a leader if not found, usually the top player of the district
            var leaderId = Guid.Parse("00000000-1111-2222-3333-444444444444"); // Placeholder District Leader ID
            
            var taxAmount = msg.FinalCleanAmount * TaxRate;

            if (taxAmount > 0)
            {
                await context.Publish(new AwardTaxCommand
                {
                    CorrelationId = msg.CorrelationId,
                    LeaderPlayerId = leaderId,
                    TaxAmount = taxAmount,
                    DistrictName = "Downtown"
                });
            }
        }
    }
}
