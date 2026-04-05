using Administration.MVC.ViewModels.ActionVMs.ActionDefinitonVMs;
using Administration.MVC.ViewModels.ActionVMs.PlayerActionAttemptVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminActionController : BaseAdminController
    {
        private readonly HttpClient _actionClient;

        public AdminActionController(IHttpClientFactory httpClientFactory)
        {
            _actionClient = httpClientFactory.CreateClient("ActionApi");
        }

        [HttpGet]
        public async Task<IActionResult> ActionDefinitions()
        {
            var result = await _actionClient
                .GetFromJsonAsync<List<ResultActionDefinitionVM>>("ActionAdmins/GetAllActionDefinitionsAsAdmin")
                ?? new List<ResultActionDefinitionVM>();

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateActionDefinition()
        {
            var vm = new CreateActionDefinitionVM { IsActive = true };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActionDefinition(CreateActionDefinitionVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _actionClient.PostAsJsonAsync("ActionAdmins/CreateActionDefinitionAsAdmin", model);
            if (!await HandleApiResultAsync(response, "Aksiyon tanımı başarıyla oluşturuldu."))
                return View(model);

            return RedirectToAction(nameof(ActionDefinitions));
        }

        [HttpGet]
        public async Task<IActionResult> EditActionDefinition(Guid id)
        {
            var dto = await _actionClient.GetFromJsonAsync<UpdateActionDefinitionVM>($"ActionAdmins/GetActionDefinitionByIdAsAdmin/{id}");
            if (dto is null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActionDefinition(UpdateActionDefinitionVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _actionClient.PutAsJsonAsync("ActionAdmins/UpdateActionDefinitionAsAdmin", model);
            if (!await HandleApiResultAsync(response, "Aksiyon tanımı başarıyla güncellendi."))
                return View(model);

            return RedirectToAction(nameof(ActionDefinitions));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActionDefinition(Guid id)
        {
            var response = await _actionClient.DeleteAsync($"ActionAdmins/DeleteActionDefinitionAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpGet]
        public async Task<IActionResult> ActionAttempts()
        {
            var result = await _actionClient
                .GetFromJsonAsync<List<ResultPlayerActionAttemptVM>>("ActionAdmins/GetAllPlayerActionAttemptsAsAdmin")
                ?? new List<ResultPlayerActionAttemptVM>();

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActionAttempt(CreatePlayerActionAttemptVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _actionClient.PostAsJsonAsync("ActionAdmins/CreatePlayerActionAttemptAsAdmin", model);
            if (!await HandleApiResultAsync(response, "Aksiyon denemesi başarıyla oluşturuldu."))
                return View(model);

            return RedirectToAction(nameof(ActionAttempts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActionAttempt(UpdatePlayerActionAttemptVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _actionClient.PutAsJsonAsync("ActionAdmins/UpdatePlayerActionAttemptAsAdmin", model);
            if (!await HandleApiResultAsync(response, "Aksiyon denemesi başarıyla güncellendi."))
                return View(model);

            return RedirectToAction(nameof(ActionAttempts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActionAttempt(Guid id)
        {
            var response = await _actionClient.DeleteAsync($"ActionAdmins/DeletePlayerActionAttemptAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }
    }
}
