using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Wallet.Commands.AdminUpdateWallet
{
    public sealed class AdminUpdateWalletCommandHandler
            : IRequestHandler<AdminUpdateWalletCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Wallet> _read;
        private readonly IWriteRepository<Domain.Entities.Wallet> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateWalletCommandHandler(
            IReadRepository<Domain.Entities.Wallet> read,
            IWriteRepository<Domain.Entities.Wallet> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateWalletDTO;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.PlayerId = d.PlayerId;
            entity.Balance = d.Balance;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();
            return ok;
        }
    }
}
