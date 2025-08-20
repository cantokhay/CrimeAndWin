using Economy.Domain.Entities;
using MediatR;

namespace Economy.Application.Features.Transactions.Queries
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {
        public Guid PlayerId { get; set; }
    }
}
