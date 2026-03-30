using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.EconomyVMs.WalletVMs
{
    public sealed class CreateWalletVM
    {
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }

        public List<SelectListItem> PlayerOptions { get; set; } = new();
    }
}
