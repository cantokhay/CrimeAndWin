using Economy.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Transactions.Commands.AdminCreateTransaction
{
    public sealed class AdminCreateTransactionCommandHandler
            : IRequestHandler<AdminCreateTransactionCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Transaction> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateTransactionCommandHandler(IWriteRepository<Domain.Entities.Transaction> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var d = request.createTransactionDTO;

            var entity = new Domain.Entities.Transaction
            {
                Id = Guid.NewGuid(),
                WalletId = d.WalletId,
                
                Money = new Money(d.Amount, d.CurrencyType),
                Reason = new TransactionReason(d.ReasonCode, d.Description),
                CreatedAtUtc = _time.UtcNow
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();
            return entity.Id;
        }
    }
}
