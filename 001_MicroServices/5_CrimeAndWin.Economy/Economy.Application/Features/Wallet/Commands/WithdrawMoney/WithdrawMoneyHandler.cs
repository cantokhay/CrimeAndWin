using Economy.Domain.Entities;
using Economy.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Commands.WithdrawMoney
{
    public class WithdrawMoneyHandler : IRequestHandler<WithdrawMoneyCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Wallet> _walletWriteRepository;
        private readonly IReadRepository<Domain.Entities.Wallet> _walletReadRepository;
        private readonly IWriteRepository<Transaction> _transactionWriteRepository;

        public WithdrawMoneyHandler(IWriteRepository<Domain.Entities.Wallet> walletWriteRepository,
                                    IReadRepository<Domain.Entities.Wallet> walletReadRepository,
                                    IWriteRepository<Transaction> transactionWriteRepository)
        {
            _walletWriteRepository = walletWriteRepository;
            _walletReadRepository = walletReadRepository;
            _transactionWriteRepository = transactionWriteRepository;
        }

        public async Task<bool> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _walletReadRepository.GetSingleAsync(w => w.PlayerId == request.PlayerId);
            if (wallet == null) return false;

            if (wallet.Balance < request.Amount) return false;

            wallet.Balance -= request.Amount;

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Money = new Money(request.Amount, request.CurrencyType),
                Reason = new TransactionReason("WITHDRAW", request.Reason)
            };

            await _transactionWriteRepository.AddAsync(transaction);
            _walletWriteRepository.Update(wallet);

            return await _transactionWriteRepository.SaveAsync() > 0;
        }

    }
}
