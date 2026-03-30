using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Wallet.Commands.AdminCreateWallet
{
    public sealed class AdminCreateWalletCommandHandler
            : IRequestHandler<AdminCreateWalletCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Wallet> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateWalletCommandHandler(IWriteRepository<Domain.Entities.Wallet> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateWalletCommand request, CancellationToken cancellationToken)
        {
            var d = request.createWalletDTO;

            var entity = new Domain.Entities.Wallet
            {
                Id = Guid.NewGuid(),
                PlayerId = d.PlayerId,
                Balance = d.Balance,
                CreatedAtUtc = _time.UtcNow,
                Transactions = new List<Domain.Entities.Transaction>()
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();
            return entity.Id;
        }
    }
}
