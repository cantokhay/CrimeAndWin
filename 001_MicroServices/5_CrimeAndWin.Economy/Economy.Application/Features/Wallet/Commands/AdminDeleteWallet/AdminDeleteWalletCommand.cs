using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Commands.AdminDeleteWallet
{
    public sealed record AdminDeleteWalletCommand(Guid id) : IRequest<bool>;
}


