using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminCooldownController(ActionApiClient actionApi) : Controller
{
    public async Task<IActionResult> Index()
    {
        var data = await actionApi.GetActiveCooldownsAsync();
        return View(data);
    }
}
