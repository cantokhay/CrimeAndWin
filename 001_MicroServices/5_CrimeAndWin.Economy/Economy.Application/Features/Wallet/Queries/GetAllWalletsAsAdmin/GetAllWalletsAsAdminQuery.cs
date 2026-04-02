using Economy.Application.DTOs.WalletDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Wallet.Queries.GetAllWalletsAsAdmin
{
    public sealed record GetAllWalletsAsAdminQuery() : IRequest<List<AdminResultWalletDTO>>;
}

