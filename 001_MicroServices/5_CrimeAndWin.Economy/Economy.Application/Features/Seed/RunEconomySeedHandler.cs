using Bogus;
using Economy.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Economy.Application.Features.Seed
{
    public sealed class RunEconomySeedHandler : IRequestHandler<RunEconomySeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Wallet> _walletRepo;
        private readonly IWriteRepository<Domain.Entities.Transaction> _transactionRepo;
        private readonly IDateTimeProvider _clock;

        public RunEconomySeedHandler(
            IWriteRepository<Domain.Entities.Wallet> walletRepo,
            IWriteRepository<Domain.Entities.Transaction> transactionRepo,
            IDateTimeProvider clock)
        {
            _walletRepo = walletRepo;
            _transactionRepo = transactionRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunEconomySeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");

            var wallets = new List<Domain.Entities.Wallet>();
            var transactions = new List<Domain.Entities.Transaction>();

            for (int i = 0; i < request.Count; i++)
            {
                var walletId = Guid.NewGuid();
                var balance = faker.Finance.Amount(100, 5000);
                var wallet = new Domain.Entities.Wallet
                {
                    Id = walletId,
                    PlayerId = Guid.NewGuid(), // fake player id
                    Balance = balance,
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false
                };

                // Her cüzdana 3–5 rastgele işlem ekleyelim
                var txCount = faker.Random.Int(3, 5);
                for (int j = 0; j < txCount; j++)
                {
                    var amount = faker.Finance.Amount(-500, 500);
                    var tx = new Domain.Entities.Transaction
                    {
                        Id = Guid.NewGuid(),
                        WalletId = walletId,
                        Money = new Money(amount, "USD"),
                        Reason = new TransactionReason(
                            faker.PickRandom("ACTION_REWARD", "ITEM_PURCHASE", "BANK_DEPOSIT", "PLAYER_TRADE"),
                            faker.Lorem.Sentence()
                        ),
                        CreatedAtUtc = _clock.UtcNow.AddMinutes(-faker.Random.Int(10, 10000)),
                        IsDeleted = false
                    };

                    transactions.Add(tx);
                }

                wallets.Add(wallet);
            }

            await _walletRepo.AddRangeAsync(wallets);
            await _transactionRepo.AddRangeAsync(transactions);

            await _walletRepo.SaveAsync();
            await _transactionRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
