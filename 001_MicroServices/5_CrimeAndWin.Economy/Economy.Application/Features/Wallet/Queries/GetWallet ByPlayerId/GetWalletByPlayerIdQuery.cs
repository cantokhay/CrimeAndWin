using Economy.Application.DTOs.WalletDTOs;
using Mediator;

namespace Economy.Application.Features.Wallet.Queries
{
    public class GetWalletByPlayerIdQuery : IRequest<ResultWalletDTO>
    {
        public Guid PlayerId { get; set; }
    }
}

