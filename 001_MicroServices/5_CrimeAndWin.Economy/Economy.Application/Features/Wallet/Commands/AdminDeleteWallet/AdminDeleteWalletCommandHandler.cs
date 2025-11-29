using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Commands.AdminDeleteWallet
{
    public sealed class AdminDeleteWalletCommandHandler
            : IRequestHandler<AdminDeleteWalletCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Wallet> _write;

        public AdminDeleteWalletCommandHandler(IWriteRepository<Domain.Entities.Wallet> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
