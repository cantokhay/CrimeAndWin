using MassTransit;
using Shared.Application.Abstractions.Messaging;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Events.Economy;
using Economy.Application.Features.Wallet.Commands.DepositMoney;

namespace Economy.API.Consumers
{
    public class RewardMoneyCommandConsumer : IConsumer<RewardMoneyCommand>
    {
        private readonly IMediator _mediator;

        public RewardMoneyCommandConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RewardMoneyCommand> context)
        {
            var msg = context.Message;
            try
            {
                var result = await _mediator.Send(new DepositMoneyCommand
                {
                    PlayerId = msg.PlayerId,
                    Amount = msg.Amount,
                    CurrencyType = "Cash",
                    Reason = msg.Reason
                });

                await context.Publish(new EconomyRewardedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = result,
                    FailReason = result ? null : "Deposit failed according to business rules."
                });
            }
            catch (Exception ex)
            {
                await context.Publish(new EconomyRewardedEvent
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


