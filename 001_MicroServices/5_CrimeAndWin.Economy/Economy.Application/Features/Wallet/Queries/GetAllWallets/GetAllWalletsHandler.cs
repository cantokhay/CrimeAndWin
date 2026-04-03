using Economy.Application.Mapping;
using Economy.Application.DTOs.WalletDTOs;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Queries.GetAllWallets
{
    public sealed class GetAllWalletsHandler(
            IReadRepository<Domain.Entities.Wallet> readRepo,
            EconomyMapper mapper)
            : IRequestHandler<GetAllWalletsQuery, List<ResultWalletDTO>>
    {
        public async Task<List<ResultWalletDTO>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            // Transactions dahil et (Include)
            var wallets = await readRepo.Table
                .Include(w => w.Transactions)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(wallets).ToList();
        }
    }
}



