using Economy.Application.DTOs.WalletDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Wallet.Commands.AdminUpdateWallet
{
    public sealed record AdminUpdateWalletCommand(AdminUpdateWalletDTO updateWalletDTO) : IRequest<bool>;
}

