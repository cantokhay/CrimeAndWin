using Administration.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers;

public class AdminSagaController(SagaApiClient sagaApi) : Controller
{
    public async Task<IActionResult> Index(string? state = null)
    {
        var data = await sagaApi.GetAllSagaStatesAsync(state);
        ViewBag.StateFilter = state;
        return View(data);
    }

    public async Task<IActionResult> Detail(Guid id)
    {
        var detail = await sagaApi.GetSagaDetailAsync(id);
        if (detail is null) return NotFound();
        return View(detail);
    }
}
