using MediatR;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Commands.AdminDeleteInventory
{
    public sealed class AdminDeleteInventoryCommandHandler
           : IRequestHandler<AdminDeleteInventoryCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Inventory> _write;

        public AdminDeleteInventoryCommandHandler(IWriteRepository<Domain.Entities.Inventory> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteInventoryCommand request, CancellationToken ct)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
