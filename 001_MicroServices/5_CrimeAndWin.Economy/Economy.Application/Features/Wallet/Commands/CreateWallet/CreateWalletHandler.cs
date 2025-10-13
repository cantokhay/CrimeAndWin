using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Commands.CreateWallet
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Wallet> _walletWriteRepository;
        private readonly IReadRepository<Domain.Entities.Wallet> _walletReadRepository;

        public CreateWalletHandler(IWriteRepository<Domain.Entities.Wallet> walletWriteRepository, IReadRepository<Domain.Entities.Wallet> walletReadRepository)
        {
            _walletWriteRepository = walletWriteRepository;
            _walletReadRepository = walletReadRepository;
        }

        public async Task<bool> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var existingWallet = await _walletReadRepository.GetSingleAsync(w => w.PlayerId == request.PlayerId);
            if (existingWallet != null)
            {
                return false;
            }
            var newWallet = new Domain.Entities.Wallet
            {
                PlayerId = request.PlayerId,
                Balance = 0,
                Transactions = new List<Domain.Entities.Transaction>()
            };
            await _walletWriteRepository.AddAsync(newWallet);
            return await _walletWriteRepository.SaveAsync() > 0;
        }
    }
}
