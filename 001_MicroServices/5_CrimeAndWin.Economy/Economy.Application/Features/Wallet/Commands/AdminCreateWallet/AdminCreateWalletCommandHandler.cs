using Economy.Application.DTOs.WalletDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Commands.AdminCreateWallet
{
    public sealed record AdminCreateWalletCommand(AdminCreateWalletDTO createWalletDTO) : IRequest<Guid>;
}


