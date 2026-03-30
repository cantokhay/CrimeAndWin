using Inventory.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Item.Commands.AdminUpdateItem
{
    public sealed class AdminUpdateItemCommandHandler
            : IRequestHandler<AdminUpdateItemCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Item> _read;
        private readonly IWriteRepository<Domain.Entities.Item> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateItemCommandHandler(
            IReadRepository<Domain.Entities.Item> read,
            IWriteRepository<Domain.Entities.Item> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateItemCommand request, CancellationToken ct)
        {
            var d = request.updateItemDTO;

            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.InventoryId = d.InventoryId;
            entity.Name = d.Name;
            entity.Quantity = d.Quantity;
            entity.Stats = new ItemStats(d.Damage, d.Defense, d.Power);
            entity.Value = new ItemValue(d.Amount, d.Currency);
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
