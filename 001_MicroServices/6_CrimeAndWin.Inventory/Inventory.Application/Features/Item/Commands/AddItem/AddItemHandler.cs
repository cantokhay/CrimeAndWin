using Inventory.Application.Messaging.Abstract;
using Inventory.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Item.Commands.AddItem
{
    public sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Item> _itemWrite;
        private readonly IEventPublisher _publisher;
        private readonly IDateTimeProvider _time;

        public AddItemCommandHandler(IWriteRepository<Domain.Entities.Item> itemWrite, IEventPublisher publisher, IDateTimeProvider time)
        {
            _itemWrite = itemWrite;
            _publisher = publisher;
            _time = time;
        }

        public async Task<bool> Handle(AddItemCommand request, CancellationToken ct)
        {
            var item = new Domain.Entities.Item
            {
                InventoryId = request.InventoryId,
                Name = request.Name,
                Quantity = request.Quantity,
                Stats = new ItemStats(request.Damage, request.Defense, request.Power),
                Value = new ItemValue(request.Amount, request.Currency),
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _itemWrite.AddAsync(item);

            //await _publisher.PublishAsync(
            //    new ItemAddedToInventoryIntegrationEvent(Guid.NewGuid(), _time.UtcNow,
            //        PlayerId: Guid.Empty, // controller/handler’da resolve edilebilir
            //        request.InventoryId, item.Id, item.Name, item.Quantity), ct);

            return await _itemWrite.SaveAsync() > 0;
        }
    }
}
