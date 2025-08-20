using MediatR;
using System.Text.Json.Serialization;

namespace Economy.Application.Features.Wallet.Commands
{
    public class WithdrawMoneyCommand : IRequest<bool>
    {
        [JsonIgnore]
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
        public string Reason { get; set; }
    }
}
