using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Inventory.Commands.AdminUpdateInventory
{
    public sealed class AdminUpdateInventoryCommandHandler
            : IRequestHandler<AdminUpdateInventoryCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Inventory> _read;
        private readonly IWriteRepository<Domain.Entities.Inventory> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateInventoryCommandHandler(
            IReadRepository<Domain.Entities.Inventory> read,
            IWriteRepository<Domain.Entities.Inventory> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateInventoryCommand request, CancellationToken ct)
        {
            var d = request.updateInventoryDTO;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), true);

            if (entity is null) return false;

            entity.PlayerId = d.PlayerId;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
