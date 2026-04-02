using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminGatewayLogController(GatewayApiClient gatewayApi) : Controller
{
    public async Task<IActionResult> Index(int count = 100)
    {
        var data = await gatewayApi.GetRecentLogsAsync(count);
        ViewBag.Count = count;
        return View(data);
    }
}
