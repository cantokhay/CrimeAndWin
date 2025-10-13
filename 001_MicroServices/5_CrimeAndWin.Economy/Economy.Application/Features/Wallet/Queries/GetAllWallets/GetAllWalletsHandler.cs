using AutoMapper;
using Economy.Application.DTOs.WalletDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Queries.GetAllWallets
{
    public sealed class GetAllWalletsHandler(
            IReadRepository<Domain.Entities.Wallet> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllWalletsQuery, List<ResultWalletDTO>>
    {
        public async Task<List<ResultWalletDTO>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            // Transactions dahil et (Include)
            var wallets = await readRepo.Table
                .Include(w => w.Transactions)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultWalletDTO>>(wallets);
        }
    }
}
