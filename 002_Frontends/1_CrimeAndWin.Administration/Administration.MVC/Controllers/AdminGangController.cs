using Microsoft.AspNetCore.Mvc;
using Administration.MVC.ViewModels.GangVMs;

namespace Administration.MVC.Controllers
{
    public class AdminGangController : Controller
    {
        public IActionResult Index()
        {
            // Mock Data for Phase 4 UI Preview (Golden Theme)
            var gangs = new List<AdminGangListVM>
            {
                new() { Id = Guid.NewGuid(), Name = "The Sopranos", Tag = "SOP", LeaderName = "Tony S.", MemberCount = 12, TotalRespect = 25000, VaultBlackBalance = 1200000, VaultCashBalance = 450000, IsActive = true },
                new() { Id = Guid.NewGuid(), Name = "Peaky Blinders", Tag = "PB", LeaderName = "Tommy S.", MemberCount = 15, TotalRespect = 32000, VaultBlackBalance = 2400000, VaultCashBalance = 800000, IsActive = true },
                new() { Id = Guid.NewGuid(), Name = "Cukur", Tag = "CKR", LeaderName = "Yamac K.", MemberCount = 10, TotalRespect = 18000, VaultBlackBalance = 500000, VaultCashBalance = 120000, IsActive = true }
            };

            return View(gangs);
        }
    }
}
