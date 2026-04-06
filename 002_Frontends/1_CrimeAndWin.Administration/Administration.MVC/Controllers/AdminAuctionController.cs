using Microsoft.AspNetCore.Mvc;
using Administration.MVC.ViewModels.AuctionVMs;

namespace Administration.MVC.Controllers
{
    public class AdminAuctionController : Controller
    {
        public IActionResult Index()
        {
            // Mock Data for Phase 4 UI Preview (Golden Theme)
            var auctions = new List<AdminAuctionListVM>
            {
                new() { Id = Guid.NewGuid(), Title = "Downtown Casino License", BasePrice = 1000000, CurrentBid = 1450000, HighestBidderName = "Tony S.", EndsAt = DateTime.UtcNow.AddHours(2), IsFinished = false },
                new() { Id = Guid.NewGuid(), Title = "Industrial Port Facility", BasePrice = 2500000, CurrentBid = 2500000, HighestBidderName = "None", EndsAt = DateTime.UtcNow.AddMinutes(45), IsFinished = false },
                new() { Id = Guid.NewGuid(), Title = "Luxury Villa (Safehouse)", BasePrice = 500000, CurrentBid = 620000, HighestBidderName = "Yamac K.", EndsAt = DateTime.UtcNow.AddDays(1), IsFinished = false }
            };

            return View(auctions);
        }
    }
}
