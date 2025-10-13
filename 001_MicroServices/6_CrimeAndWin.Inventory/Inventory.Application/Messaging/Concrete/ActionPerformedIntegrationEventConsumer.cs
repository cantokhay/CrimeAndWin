using Inventory.Application.Features.Item.Commands.AddItem;
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

            foreach (var r in ev.Items)
            {
                var cmd = new AddItemCommand(
                InventoryId: Guid.Empty,
                    r.Name, r.Quantity, r.Damage, r.Defense, r.Power, r.Amount, (CurrencyType)r.Currency);
                await _mediator.Send(cmd, context.CancellationToken);
            }
        }
    }
}
