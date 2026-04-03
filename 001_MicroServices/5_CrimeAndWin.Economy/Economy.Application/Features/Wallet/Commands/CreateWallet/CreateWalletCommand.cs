using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Commands.CreateWallet
{
    public class CreateWalletCommand : IRequest<bool>
    {
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
    }
}


