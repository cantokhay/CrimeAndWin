using Economy.Application.DTOs.WalletDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Queries.GetWalletByIdAsAdmin
{
    public sealed class GetWalletByIdAsAdminQueryHandler
            : IRequestHandler<GetWalletByIdAsAdminQuery, AdminResultWalletDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Wallet> _read;

        public GetWalletByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Wallet> read)
        {
            _read = read;
        }

        public async Task<AdminResultWalletDTO?> Handle(GetWalletByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var w = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (w is null) return null;

            return new AdminResultWalletDTO
            {
                Id = w.Id,
                PlayerId = w.PlayerId,
                Balance = w.Balance,
                CreatedAtUtc = w.CreatedAtUtc,
                UpdatedAtUtc = w.UpdatedAtUtc
            };
        }
    }
}
