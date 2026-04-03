using Economy.Application.DTOs.WalletDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Commands.AdminUpdateWallet
{
    public sealed record AdminUpdateWalletCommand(AdminUpdateWalletDTO updateWalletDTO) : IRequest<bool>;
}


