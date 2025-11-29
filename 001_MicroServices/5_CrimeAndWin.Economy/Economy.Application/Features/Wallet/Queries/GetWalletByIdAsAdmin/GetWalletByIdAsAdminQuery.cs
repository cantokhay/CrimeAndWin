using Economy.Application.DTOs.WalletDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Wallet.Queries.GetWalletByIdAsAdmin
{
    public sealed record GetWalletByIdAsAdminQuery(Guid id) : IRequest<AdminResultWalletDTO?>;
}
