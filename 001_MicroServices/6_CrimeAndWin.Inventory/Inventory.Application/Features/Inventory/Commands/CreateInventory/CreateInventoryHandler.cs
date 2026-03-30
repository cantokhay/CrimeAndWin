using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Inventory.Commands.CreateInventory
{
    public sealed class CreateInventoryHandler : IRequestHandler<CreateInventoryCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Inventory> _write;
        private readonly IReadRepository<Domain.Entities.Inventory> _read;
        private readonly IDateTimeProvider _time;

        public CreateInventoryHandler(IWriteRepository<Domain.Entities.Inventory> write, IDateTimeProvider time, IReadRepository<Domain.Entities.Inventory> read)
        {
            _write = write;
            _time = time;
            _read = read;
        }

        public async Task<bool> Handle(CreateInventoryCommand request, CancellationToken ct)
        {
            var existingInventory = await _read.GetSingleAsync(i => i.PlayerId == request.PlayerId);
            if (existingInventory != null)
            {
                return false;
            }
            var inv = new Domain.Entities.Inventory
            {
                PlayerId = request.PlayerId,
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false,
                Items = new List<Domain.Entities.Item>()
            };

            await _write.AddAsync(inv);
            return await _write.SaveAsync() > 0;
        }
    }
}
