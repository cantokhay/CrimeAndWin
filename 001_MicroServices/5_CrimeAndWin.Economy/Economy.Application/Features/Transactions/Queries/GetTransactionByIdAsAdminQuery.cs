using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Transactions.Queries
{
    public sealed record GetTransactionByIdAsAdminQuery(Guid id) : IRequest<AdminResultTransactionDTO?>;
}
