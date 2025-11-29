using Economy.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Transactions.Commands.AdminUpdateTransaction
{
    public sealed class AdminUpdateTransactionCommandHandler
            : IRequestHandler<AdminUpdateTransactionCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Transaction> _read;
        private readonly IWriteRepository<Domain.Entities.Transaction> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateTransactionCommandHandler(
            IReadRepository<Domain.Entities.Transaction> read,
            IWriteRepository<Domain.Entities.Transaction> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateTransactionDTO;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.WalletId = d.WalletId;
            entity.Money = new Money(d.Amount, d.CurrencyType);
            entity.Reason = new TransactionReason(d.ReasonCode, d.Description);
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();
            return ok;
        }
    }
}
