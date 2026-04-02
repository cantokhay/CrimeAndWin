using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminActionLogController(ActionApiClient actionApi) : Controller
{
    public async Task<IActionResult> Index(int page = 1)
    {
        var data = await actionApi.GetActionLogsAsync(page, pageSize: 50);
        ViewBag.CurrentPage = page;
        return View(data);
    }
}
