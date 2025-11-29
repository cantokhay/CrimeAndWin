using Economy.Application.DTOs.WalletDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Wallet.Commands.AdminCreateWallet
{
    public sealed record AdminCreateWalletCommand(AdminCreateWalletDTO createWalletDTO) : IRequest<Guid>;
}
