using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Transactions.Queries
{
    public sealed class GetTransactionByIdAsAdminQueryHandler
            : IRequestHandler<GetTransactionByIdAsAdminQuery, AdminResultTransactionDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Transaction> _read;

        public GetTransactionByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Transaction> read)
        {
            _read = read;
        }

        public async Task<AdminResultTransactionDTO?> Handle(GetTransactionByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var t = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (t is null) return null;

            return new AdminResultTransactionDTO
            {
                Id = t.Id,
                WalletId = t.WalletId,
                Amount = t.Money.Amount,
                CurrencyType = t.Money.CurrencyType,
                ReasonCode = t.Reason.ReasonCode,
                Description = t.Reason.Description,
                CreatedAtUtc = t.CreatedAtUtc,
                UpdatedAtUtc = t.UpdatedAtUtc
            };
        }
    }
}
