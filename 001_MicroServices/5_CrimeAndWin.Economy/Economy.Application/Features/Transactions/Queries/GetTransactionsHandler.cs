using Economy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Economy.Application.Features.Transactions.Queries
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        //private readonly IReadRepository<Domain.Entities.Wallet> _walletReadRepository;
        private readonly IReadRepository<Domain.Entities.Transaction> _txReadRepository;

        public GetTransactionsHandler(IReadRepository<Transaction> txReadRepository)
        {
            _txReadRepository = txReadRepository;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            //var wallet = await _walletReadRepository.GetSingleAsync(w => w.PlayerId == request.PlayerId);
            //return wallet?.Transactions.ToList() ?? new List<Transaction>();
            return await _txReadRepository.Table
                //.Where(t => t. == request.Wa)
                .ToListAsync(cancellationToken);
        }
    }
}
