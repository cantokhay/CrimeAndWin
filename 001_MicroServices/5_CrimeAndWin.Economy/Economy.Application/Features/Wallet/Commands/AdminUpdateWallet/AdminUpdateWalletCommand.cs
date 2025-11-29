using Economy.Application.DTOs.WalletDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Wallet.Commands.AdminUpdateWallet
{
    public sealed record AdminUpdateWalletCommand(AdminUpdateWalletDTO updateWalletDTO) : IRequest<bool>;
}
