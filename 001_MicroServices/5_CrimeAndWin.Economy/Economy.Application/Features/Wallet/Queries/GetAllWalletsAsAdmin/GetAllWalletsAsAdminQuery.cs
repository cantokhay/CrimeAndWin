using Economy.Application.DTOs.WalletDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Queries.GetAllWalletsAsAdmin
{
    public sealed record GetAllWalletsAsAdminQuery() : IRequest<List<AdminResultWalletDTO>>;
}


