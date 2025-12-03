using MediatR;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Item.Commands.AdminDeleteItem
{
    public sealed class AdminDeleteItemCommandHandler
            : IRequestHandler<AdminDeleteItemCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Item> _write;

        public AdminDeleteItemCommandHandler(IWriteRepository<Domain.Entities.Item> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteItemCommand request, CancellationToken ct)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
