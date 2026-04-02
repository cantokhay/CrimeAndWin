using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminHealthController(HealthApiClient healthApi) : Controller
{
    public async Task<IActionResult> Index()
    {
        var data = await healthApi.GetAllHealthAsync();
        return View(data);
    }
}
