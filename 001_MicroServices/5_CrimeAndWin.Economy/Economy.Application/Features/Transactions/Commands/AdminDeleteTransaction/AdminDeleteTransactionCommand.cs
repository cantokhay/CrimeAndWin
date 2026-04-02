using Mediator;

namespace Economy.Application.Features.Transactions.Commands.AdminDeleteTransaction
{
    public sealed record AdminDeleteTransactionCommand(Guid id) : IRequest<bool>;
}

