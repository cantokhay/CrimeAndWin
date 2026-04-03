using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Commands.AdminDeleteTransaction
{
    public sealed record AdminDeleteTransactionCommand(Guid id) : IRequest<bool>;
}


