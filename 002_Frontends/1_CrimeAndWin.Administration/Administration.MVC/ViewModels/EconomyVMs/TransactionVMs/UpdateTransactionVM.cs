using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.EconomyVMs.TransactionVMs
{
    public sealed class UpdateTransactionVM
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; } = null!;
        public string ReasonCode { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<SelectListItem> WalletOptions { get; set; } = new();
    }
}
