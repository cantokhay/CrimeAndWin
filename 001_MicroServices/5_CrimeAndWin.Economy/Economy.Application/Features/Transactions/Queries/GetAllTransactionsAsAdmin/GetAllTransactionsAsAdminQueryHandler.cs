using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Transactions.Queries.GetAllTransactionsAsAdmin
{
    public sealed class GetAllTransactionsAsAdminQueryHandler
            : IRequestHandler<GetAllTransactionsAsAdminQuery, List<AdminResultTransactionDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Transaction> _read;

        public GetAllTransactionsAsAdminQueryHandler(IReadRepository<Domain.Entities.Transaction> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultTransactionDTO>> Handle(GetAllTransactionsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(t => new AdminResultTransactionDTO
                {
                    Id = t.Id,
                    WalletId = t.WalletId,
                    Amount = t.Money.Amount,
                    CurrencyType = t.Money.CurrencyType,
                    ReasonCode = t.Reason.ReasonCode,
                    Description = t.Reason.Description,
                    CreatedAtUtc = t.CreatedAtUtc,
                    UpdatedAtUtc = t.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
