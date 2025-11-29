using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Transactions.Queries.GetAllTransactionsAsAdmin
{
    public sealed record GetAllTransactionsAsAdminQuery() : IRequest<List<AdminResultTransactionDTO>>;
}

