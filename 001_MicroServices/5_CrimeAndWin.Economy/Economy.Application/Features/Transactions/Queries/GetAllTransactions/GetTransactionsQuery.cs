using Economy.Domain.Entities;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Queries.GetAllTransactions
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {
        public Guid PlayerId { get; set; }
    }
}


