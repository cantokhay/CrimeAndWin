using Economy.Application.DTOs.TransactionDTOs;
using Economy.Application.DTOs.WalletDTOs;
using Economy.Domain.Entities;
using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Transactions.Queries.GetTransactionByPlayerId
{
    public class GetTransactionsByPlayerIdQuery : IRequest<List<ResultTransactionDTO>>
    {
        public Guid PlayerId { get; set; }
    }
}


