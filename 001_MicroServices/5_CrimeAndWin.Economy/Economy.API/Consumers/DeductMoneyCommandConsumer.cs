using MassTransit;
using Mediator;
using CrimeAndWin.Contracts.Commands.Economy;
using CrimeAndWin.Contracts.Events.Economy;
using Economy.Application.Features.Wallet.Commands.WithdrawMoney;

namespace Economy.API.Consumers
{
    public class DeductMoneyCommandConsumer : IConsumer<DeductMoneyCommand>
    {
        private readonly IMediator _mediator;

        public DeductMoneyCommandConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<DeductMoneyCommand> context)
        {
            var msg = context.Message;
            try
            {
                var result = await _mediator.Send(new WithdrawMoneyCommand
                {
                    PlayerId = msg.PlayerId,
                    Amount = msg.Amount,
                    CurrencyType = "Cash",
                    Reason = msg.Reason
                });

                await context.Publish(new MoneyDeductedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = result,
                    FailReason = result ? null : "Withdraw failed (insufficient funds or missing wallet)."
                });
            }
            catch (Exception ex)
            {
                await context.Publish(new MoneyDeductedEvent
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

