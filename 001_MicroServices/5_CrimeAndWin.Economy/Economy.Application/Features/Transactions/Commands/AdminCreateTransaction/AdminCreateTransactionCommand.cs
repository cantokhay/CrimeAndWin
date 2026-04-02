using Economy.Application.DTOs.TransactionDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Transactions.Commands.AdminCreateTransaction
{
    public sealed record AdminCreateTransactionCommand(AdminCreateTransactionDTO createTransactionDTO) : IRequest<Guid>;
}

