using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Constants;
using Economy.Domain.Entities;
using Economy.Domain.VOs;
using Microsoft.EntityFrameworkCore;

namespace Economy.Application.Features.Seed
{
    public sealed class RunEconomySeedHandler : IRequestHandler<RunEconomySeedCommand, Unit>
    {
        private readonly IWriteRepository<Economy.Domain.Entities.Wallet> _walletWrite;
        private readonly IReadRepository<Economy.Domain.Entities.Wallet> _walletRead;
        private readonly IWriteRepository<Transaction> _transactionWrite;
        private readonly IReadRepository<Transaction> _transactionRead;

        public RunEconomySeedHandler(
            IWriteRepository<Economy.Domain.Entities.Wallet> walletWrite,
            IReadRepository<Economy.Domain.Entities.Wallet> walletRead,
            IWriteRepository<Transaction> transactionWrite,
            IReadRepository<Transaction> transactionRead)
        {
            _walletWrite = walletWrite;
            _walletRead = walletRead;
            _transactionWrite = transactionWrite;
            _transactionRead = transactionRead;
        }

        public async Task<Unit> Handle(RunEconomySeedCommand request, CancellationToken ct)
        {
            var seedDate = SeedDataConstants.SeedDate;

            // Core Wallets
            var coreWallets = new List<Economy.Domain.Entities.Wallet>
            {
                new()
                {
                    Id = SeedDataConstants.WalletAlphaId,
                    PlayerId = SeedDataConstants.PlayerAlphaId,
                    Balance = 50000,
                    BlackBalance = 10000,
                    CashBalance = 40000,
                    CreatedAtUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.WalletBetaId,
                    PlayerId = SeedDataConstants.PlayerBetaId,
                    Balance = 25000,
                    BlackBalance = 5000,
                    CashBalance = 20000,
                    CreatedAtUtc = seedDate
                }
            };

            foreach (var w in coreWallets)
            {
                if (await _walletRead.GetByIdAsync(w.Id.ToString()) == null)
                {
                    await _walletWrite.AddAsync(w);
                }
            }
            await _walletWrite.SaveAsync();

            // Core Transactions
            var coreTransactions = new List<Transaction>
            {
                new()
                {
                    Id = SeedDataConstants.TransactionAlpha1Id,
                    WalletId = SeedDataConstants.WalletAlphaId,
                    Money = new Money(10000, "CrimeReward"),
                    Reason = new TransactionReason("INITIAL_CRIME", "Reward for the first mission."),
                    BalanceType = Economy.Domain.Enums.WalletBalanceType.BlackMoney,
                    CreatedAtUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.TransactionBeta1Id,
                    WalletId = SeedDataConstants.WalletBetaId,
                    Money = new Money(5000, "CrimeReward"),
                    Reason = new TransactionReason("INITIAL_CRIME", "Reward for the first mission."),
                    BalanceType = Economy.Domain.Enums.WalletBalanceType.BlackMoney,
                    CreatedAtUtc = seedDate
                }
            };

            foreach (var t in coreTransactions)
            {
                if (await _transactionRead.GetByIdAsync(t.Id.ToString()) == null)
                {
                    await _transactionWrite.AddAsync(t);
                }
            }
            await _transactionWrite.SaveAsync();

            return Unit.Value;
        }
    }
}
