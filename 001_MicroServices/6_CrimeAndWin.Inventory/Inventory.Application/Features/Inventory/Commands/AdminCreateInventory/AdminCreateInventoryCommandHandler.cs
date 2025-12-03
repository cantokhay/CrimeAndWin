using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Inventory.Commands.AdminCreateInventory
{
    public sealed class AdminCreateInventoryCommandHandler
            : IRequestHandler<AdminCreateInventoryCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Inventory> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateInventoryCommandHandler(IWriteRepository<Domain.Entities.Inventory> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateInventoryCommand request, CancellationToken ct)
        {
            var d = request.createInventoryDTO;

            var inv = new Domain.Entities.Inventory
            {
                Id = Guid.NewGuid(),
                PlayerId = d.PlayerId,
                Items = new(),
                CreatedAtUtc = _time.UtcNow
            };

            await _write.AddAsync(inv);
            await _write.SaveAsync();

            return inv.Id;
        }
    }
}
