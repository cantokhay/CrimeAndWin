using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Transactions.Commands.AdminCreateTransaction
{
    public sealed record AdminCreateTransactionCommand(AdminCreateTransactionDTO createTransactionDTO) : IRequest<Guid>;
}
