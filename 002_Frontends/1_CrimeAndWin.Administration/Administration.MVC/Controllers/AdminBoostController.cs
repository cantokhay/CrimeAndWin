using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminBoostController(ActionApiClient actionApi) : Controller
{
    public async Task<IActionResult> Index()
    {
        var data = await actionApi.GetActiveBoostsAsync();
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Revoke(Guid playerId)
    {
        await actionApi.RevokeBoostAsync(playerId);
        TempData["Success"] = "Boost iptal edildi.";
        return RedirectToAction(nameof(Index));
    }
}
