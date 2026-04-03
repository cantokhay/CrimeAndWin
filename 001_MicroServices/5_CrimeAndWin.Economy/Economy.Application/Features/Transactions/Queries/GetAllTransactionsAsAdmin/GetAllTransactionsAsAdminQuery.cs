using Economy.Application.DTOs.TransactionDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Queries.GetAllTransactionsAsAdmin
{
    public sealed record GetAllTransactionsAsAdminQuery() : IRequest<List<AdminResultTransactionDTO>>;
}



