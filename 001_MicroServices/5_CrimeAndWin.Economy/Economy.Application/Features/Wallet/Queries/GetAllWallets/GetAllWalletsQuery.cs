using Economy.Application.DTOs.WalletDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Queries.GetAllWallets
{
    public sealed record GetAllWalletsQuery() : IRequest<List<ResultWalletDTO>>;
}


