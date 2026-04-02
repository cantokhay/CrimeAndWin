using Economy.Domain.Entities;
using Mediator;

namespace Economy.Application.Features.Transactions.Queries.GetAllTransactions
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {
        public Guid PlayerId { get; set; }
    }
}

