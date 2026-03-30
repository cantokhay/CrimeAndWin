using Administration.MVC.ViewModels.ActionVMs.ActionDefinitonVMs;
using Administration.MVC.ViewModels.ActionVMs.PlayerActionAttemptVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminActionController : Controller
    {
        private readonly HttpClient _actionClient;

        public AdminActionController(IHttpClientFactory httpClientFactory)
        {
            _actionClient = httpClientFactory.CreateClient("ActionApi");
        }

        // ============================
        // ACTION DEFINITIONS
        // ============================

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
            var vm = new CreateActionDefinitionVM
            {
                IsActive = true
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActionDefinition(CreateActionDefinitionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _actionClient.PostAsJsonAsync("ActionAdmins/CreateActionDefinitionAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Oluşturma hatası: {(int)response.StatusCode} - {body}");
                return View(model);
            }

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
            if (!ModelState.IsValid)
                return View(model);

            var response = await _actionClient.PutAsJsonAsync("ActionAdmins/UpdateActionDefinitionAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Güncelleme hatası: {(int)response.StatusCode} - {body}");
                return View(model);
            }

            return RedirectToAction(nameof(ActionDefinitions));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActionDefinition(Guid id)
        {
            var response = await _actionClient.DeleteAsync($"ActionAdmins/DeleteActionDefinitionAsAdmin/{id}");

            return Json(new { success = response.IsSuccessStatusCode });
        }

        // =============================
        // PLAYER ACTION ATTEMPTS
        // =============================

        [HttpGet]
        public async Task<IActionResult> ActionAttempts()
        {
            var result = await _actionClient
                .GetFromJsonAsync<List<ResultPlayerActionAttemptVM>>("ActionAdmins/GetAllPlayerActionAttemptsAsAdmin")
                ?? new List<ResultPlayerActionAttemptVM>();

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateActionAttempt()
        {
            var vm = new CreatePlayerActionAttemptVM
            {
                SuccessRate = 0.5
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateActionAttempt(CreatePlayerActionAttemptVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _actionClient.PostAsJsonAsync("ActionAdmins/CreatePlayerActionAttemptAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Oluşturma hatası: {(int)response.StatusCode} - {body}");
                return View(model);
            }

            return RedirectToAction(nameof(ActionAttempts));
        }

        [HttpGet]
        public async Task<IActionResult> EditActionAttempt(Guid id)
        {
            var dto = await _actionClient.GetFromJsonAsync<UpdatePlayerActionAttemptVM>($"ActionAdmins/GetPlayerActionAttemptByIdAsAdmin/{id}");
            if (dto is null) return NotFound();

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditActionAttempt(UpdatePlayerActionAttemptVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _actionClient.PutAsJsonAsync("ActionAdmins/UpdatePlayerActionAttemptAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Güncelleme hatası: {(int)response.StatusCode} - {body}");
                return View(model);
            }

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
