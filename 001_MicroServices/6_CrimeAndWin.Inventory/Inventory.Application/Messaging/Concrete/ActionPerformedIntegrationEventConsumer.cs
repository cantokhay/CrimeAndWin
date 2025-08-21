using Inventory.Application.Features.Item.Commands;
using Inventory.Domain.Enums;
using MassTransit;
using MediatR;

namespace Inventory.Application.Messaging.Concrete
{
    public sealed class ActionPerformedIntegrationEventConsumer : IConsumer<ActionPerformedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        public ActionPerformedIntegrationEventConsumer(IMediator mediator) => _mediator = mediator;

        public async Task Consume(ConsumeContext<ActionPerformedIntegrationEvent> context)
        {
            var ev = context.Message;
            if (ev.Items is null || ev.Items.Count == 0) return;

            // burada ilgili PlayerId’nin envanterini bulup item’ları eklemek için
            // önce envanteri bul/determine et, ardından AddItemCommand gönder:
            foreach (var r in ev.Items)
            {
                var cmd = new AddItemCommand(
                // You must provide a valid Guid for InventoryId. 
                // If you have a way to resolve InventoryId from the playerId in the event, use it here.
                // For example, if ev.PlayerId exists and you have a method to get InventoryId from it:
                // var inventoryId = await _inventoryService.GetInventoryIdByPlayerId(ev.PlayerId);

                // Replace the following line with the correct InventoryId resolution:
                InventoryId: Guid.Empty, // <-- Replace Guid.Empty with the actual InventoryId
                    r.Name, r.Quantity, r.Damage, r.Defense, r.Power, r.Amount, (CurrencyType)r.Currency);
                await _mediator.Send(cmd, context.CancellationToken);
            }
        }
    }
}
