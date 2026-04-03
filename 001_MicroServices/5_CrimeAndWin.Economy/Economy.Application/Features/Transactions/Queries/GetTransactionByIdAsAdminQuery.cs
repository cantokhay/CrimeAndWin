using Economy.Application.DTOs.TransactionDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Queries
{
    public sealed record GetTransactionByIdAsAdminQuery(Guid id) : IRequest<AdminResultTransactionDTO?>;
}


