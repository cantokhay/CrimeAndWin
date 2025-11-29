using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Transactions.Commands.AdminDeleteTransaction
{
    public sealed class AdminDeleteTransactionCommandHandler
            : IRequestHandler<AdminDeleteTransactionCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Transaction> _write;

        public AdminDeleteTransactionCommandHandler(IWriteRepository<Domain.Entities.Transaction> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
