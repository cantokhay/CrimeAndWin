using Economy.Application.DTOs.TransactionDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Transactions.Queries
{
    public sealed record GetTransactionByIdAsAdminQuery(Guid id) : IRequest<AdminResultTransactionDTO?>;
}

