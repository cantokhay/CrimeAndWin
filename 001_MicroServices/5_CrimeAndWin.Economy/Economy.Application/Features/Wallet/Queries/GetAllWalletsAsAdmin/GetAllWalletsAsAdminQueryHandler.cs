using Economy.Application.DTOs.WalletDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Queries.GetAllWalletsAsAdmin
{
    public sealed class GetAllWalletsAsAdminQueryHandler
            : IRequestHandler<GetAllWalletsAsAdminQuery, List<AdminResultWalletDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Wallet> _read;

        public GetAllWalletsAsAdminQueryHandler(IReadRepository<Domain.Entities.Wallet> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultWalletDTO>> Handle(GetAllWalletsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(w => new AdminResultWalletDTO
                {
                    Id = w.Id,
                    PlayerId = w.PlayerId,
                    Balance = w.Balance,
                    CreatedAtUtc = w.CreatedAtUtc,
                    UpdatedAtUtc = w.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
