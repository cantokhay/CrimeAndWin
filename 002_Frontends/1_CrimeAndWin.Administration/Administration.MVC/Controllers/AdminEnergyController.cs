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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManualRefill(Guid playerId, int amount)
    {
        try
        {
            await actionApi.ManualEnergyRefillAsync(playerId, amount);
            return Json(new { success = true, message = $"Oyuncuya {amount} enerji eklendi." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Hata: {ex.Message}" });
        }
    }
}
