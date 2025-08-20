using Economy.Application.DTOs.TransactionDTOs;
using Economy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Transactions.Queries
{
    public class GetTransactionsByPlayerIdHandler
            : IRequestHandler<GetTransactionsByPlayerIdQuery, List<ResultTransactionDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Transaction> _tx;

        public GetTransactionsByPlayerIdHandler(IReadRepository<Domain.Entities.Transaction> tx)
        {
            _tx = tx;
        }

        public async Task<List<ResultTransactionDTO>> Handle(GetTransactionsByPlayerIdQuery request, CancellationToken ct)
        {
            return await _tx.Table
                .AsNoTracking()
                .Where(t => t.Wallet.PlayerId == request.PlayerId)
                .OrderByDescending(t => t.CreatedAtUtc)
                .Select(t => new ResultTransactionDTO
                {
                    Id = t.Id,
                    WalletId = t.WalletId,
                    Amount = t.Money.Amount,
                    Type = t.Money.CurrencyType,
                    CreatedAtUtc = t.CreatedAtUtc,
                    Description = t.Reason.Description,
                    ReasonCode = t.Reason.ReasonCode
                })
                .ToListAsync(ct);
        }
    }
}
