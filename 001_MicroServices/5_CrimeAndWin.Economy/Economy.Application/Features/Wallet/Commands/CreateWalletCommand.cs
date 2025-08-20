using MediatR;

namespace Economy.Application.Features.Wallet.Commands
{
    public class CreateWalletCommand : IRequest<bool>
    {
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
    }
}
