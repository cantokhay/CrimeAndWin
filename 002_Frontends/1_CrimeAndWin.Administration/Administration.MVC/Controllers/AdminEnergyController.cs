using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminEnergyController(ActionApiClient actionApi) : Controller
{
    public async Task<IActionResult> Index()
    {
        var data = await actionApi.GetAllEnergyStatesAsync();
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> ManualRefill(Guid playerId, int amount)
    {
        await actionApi.ManualEnergyRefillAsync(playerId, amount);
        TempData["Success"] = $"Oyuncuya {amount} enerji eklendi.";
        return RedirectToAction(nameof(Index));
    }
}
