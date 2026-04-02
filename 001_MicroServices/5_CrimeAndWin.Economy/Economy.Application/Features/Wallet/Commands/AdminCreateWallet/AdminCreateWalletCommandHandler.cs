using Economy.Application.DTOs.WalletDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Wallet.Commands.AdminCreateWallet
{
    public sealed record AdminCreateWalletCommand(AdminCreateWalletDTO createWalletDTO) : IRequest<Guid>;
}

