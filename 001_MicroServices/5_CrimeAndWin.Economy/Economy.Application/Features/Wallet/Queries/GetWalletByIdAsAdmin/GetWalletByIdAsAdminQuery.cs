using Economy.Application.DTOs.WalletDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Wallet.Queries.GetWalletByIdAsAdmin
{
    public sealed record GetWalletByIdAsAdminQuery(Guid id) : IRequest<AdminResultWalletDTO?>;
}


