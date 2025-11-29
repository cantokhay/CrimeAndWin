using Economy.Application.DTOs.TransactionDTOs.Admin;
using MediatR;

namespace Economy.Application.Features.Transactions.Commands.AdminUpdateTransaction
{
    public sealed record AdminUpdateTransactionCommand(AdminUpdateTransactionDTO updateTransactionDTO) : IRequest<bool>;
}
