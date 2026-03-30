using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.EconomyVMs.WalletVMs
{
    public sealed class UpdateWalletVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }

        public List<SelectListItem> PlayerOptions { get; set; } = new();
    }
}
