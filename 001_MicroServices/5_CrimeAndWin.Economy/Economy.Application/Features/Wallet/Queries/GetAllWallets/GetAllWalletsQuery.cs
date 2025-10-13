using Economy.Application.DTOs.WalletDTOs;
using MediatR;

namespace Economy.Application.Features.Wallet.Queries.GetAllWallets
{
    public sealed record GetAllWalletsQuery() : IRequest<List<ResultWalletDTO>>;
}
