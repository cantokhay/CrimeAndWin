using MassTransit;
using Mediator;
using CrimeAndWin.Contracts.Commands.Inventory;
using CrimeAndWin.Contracts.Events.Inventory;
using Inventory.Application.Features.Item.Commands.AdminDeleteItem;

namespace Inventory.API.Consumers
{
    public class RevokeItemCommandConsumer : IConsumer<RevokeItemCommand>
    {
        private readonly IMediator _mediator;

        public RevokeItemCommandConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RevokeItemCommand> context)
        {
            var msg = context.Message;
            try
            {
                var result = await _mediator.Send(new AdminDeleteItemCommand(msg.ItemId));

                await context.Publish(new InventoryGrantedEvent // SOP indicated to just publish InventoryGrantedEvent as the end of this module's flow if needed, but wait: Is success mapped? Rollback events usually don't need to trigger the forward progress event. I will emit it just in case with IsSuccess=false so saga knows it failed? No, for compensation, emitting the event isn't strictly listened to by the saga, but we do it to complete the cycle.
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = result,
                    FailReason = result ? null : "Item could not be deleted/revoked."
                });
            }
            catch (Exception ex)
            {
                await context.Publish(new InventoryGrantedEvent
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

