using Economy.Domain.Entities;
using Economy.Domain.VOs;
using Bogus;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Seed
{
    public sealed class RunEconomySeedHandler : IRequestHandler<RunEconomySeedCommand, Unit>
    {
        private readonly IWriteRepository<Economy.Domain.Entities.Wallet> _walletRepo;
        private readonly IWriteRepository<Transaction> _transactionRepo;
        private readonly IDateTimeProvider _clock;

        public RunEconomySeedHandler(
            IWriteRepository<Economy.Domain.Entities.Wallet> walletRepo,
            IWriteRepository<Transaction> transactionRepo,
            IDateTimeProvider clock)
        {
            _walletRepo = walletRepo;
            _transactionRepo = transactionRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunEconomySeedCommand request, CancellationToken ct)
        {
            var now = _clock.UtcNow;
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            
            var wallets = new List<Economy.Domain.Entities.Wallet>();
            var transactions = new List<Transaction>();

            for (int i = 0; i < themeUsers.Length; i++)
            {
                var walletId = Guid.Parse($"44444444-4444-4444-4444-{i:D12}");
                var playerId = Guid.Parse($"22222222-2222-2222-2222-{i:D12}"); // From PlayerProfile
                
                var wallet = new Economy.Domain.Entities.Wallet
                {
                    Id = walletId,
                    PlayerId = playerId,
                    Balance = 5000 * (i + 1),
                    BlackBalance = 0,
                    CashBalance = 5000 * (i + 1), // Seed some cash
                    CreatedAtUtc = now
                };

                // Add a "Welcome" transaction
                transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    WalletId = walletId,
                    Money = new Money(wallet.Balance, "Cash"),
                    Reason = new TransactionReason("INITIAL_FUND", "Starting capital from the Boss."),
                    BalanceType = Economy.Domain.Enums.WalletBalanceType.CashBalance,
                    CreatedAtUtc = now.AddDays(-1)
                });
                
                wallets.Add(wallet);
            }

            try {
                await _walletRepo.AddRangeAsync(wallets);
                await _transactionRepo.AddRangeAsync(transactions);
                await _walletRepo.SaveAsync();
                await _transactionRepo.SaveAsync();
            } catch { }

            return Unit.Value;
        }
    }
}
