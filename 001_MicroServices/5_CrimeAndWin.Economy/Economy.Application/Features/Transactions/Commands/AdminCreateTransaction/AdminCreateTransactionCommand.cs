using Economy.Application.DTOs.TransactionDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Commands.AdminCreateTransaction
{
    public sealed record AdminCreateTransactionCommand(AdminCreateTransactionDTO createTransactionDTO) : IRequest<Guid>;
}


