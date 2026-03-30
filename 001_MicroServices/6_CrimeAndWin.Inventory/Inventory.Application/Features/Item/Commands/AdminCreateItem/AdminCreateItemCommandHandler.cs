using Inventory.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Item.Commands.AdminCreateItem
{
    public sealed class AdminCreateItemCommandHandler
            : IRequestHandler<AdminCreateItemCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Item> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateItemCommandHandler(
            IWriteRepository<Domain.Entities.Item> write,
            IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateItemCommand request, CancellationToken ct)
        {
            var d = request.createItemDTO;

            var entity = new Domain.Entities.Item
            {
                Id = Guid.NewGuid(),
                InventoryId = d.InventoryId,
                Name = d.Name,
                Quantity = d.Quantity,
                Stats = new ItemStats(d.Damage, d.Defense, d.Power),
                Value = new ItemValue(d.Amount, d.Currency),
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
