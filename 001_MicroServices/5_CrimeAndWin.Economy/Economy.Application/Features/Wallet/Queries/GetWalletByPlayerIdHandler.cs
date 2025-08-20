using Economy.Application.DTOs.TransactionDTOs;
using Economy.Application.DTOs.WalletDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Wallet.Queries
{
    public class GetWalletByPlayerIdHandler : IRequestHandler<GetWalletByPlayerIdQuery, ResultWalletDTO>
    {
        private readonly IReadRepository<Domain.Entities.Wallet> _wallets;

        public GetWalletByPlayerIdHandler(IReadRepository<Domain.Entities.Wallet> wallets)
        {
            _wallets = wallets;
        }

        public async Task<ResultWalletDTO?> Handle(GetWalletByPlayerIdQuery request, CancellationToken ct)
        {
            return await _wallets.Table
                .AsNoTracking()
                .Where(w => w.PlayerId == request.PlayerId)
                .Select(w => new ResultWalletDTO
                {
                    Id = w.Id,
                    PlayerId = w.PlayerId,
                    Balance = w.Balance,
                    Transactions = w.Transactions
                        .OrderByDescending(t => t.CreatedAtUtc)
                        .Select(t => new ResultTransactionDTO
                        {
                            Id = t.Id,
                            WalletId = t.WalletId,
                            Amount = t.Money.Amount,
                            Type = t.Money.CurrencyType,
                            CreatedAtUtc = t.CreatedAtUtc
                        }).ToList()
                })
                .FirstOrDefaultAsync(ct);
        }
    }
}
