using Economy.Application.DTOs.TransactionDTOs.Admin;
using Mediator;

namespace Economy.Application.Features.Transactions.Commands.AdminUpdateTransaction
{
    public sealed record AdminUpdateTransactionCommand(AdminUpdateTransactionDTO updateTransactionDTO) : IRequest<bool>;
}

