using Administration.MVC.ViewModels.NotificationVMs.NotificationVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminNotificationController : Controller
    {
        private readonly HttpClient _notificationClient;
        private readonly HttpClient _playerClient;

        public AdminNotificationController(IHttpClientFactory httpClientFactory)
        {
            _notificationClient = httpClientFactory.CreateClient("NotificationApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
        }

        // ============================
        // NOTIFICATION LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Notifications()
        {
            var list = await _notificationClient
                .GetFromJsonAsync<List<ResultNotificationVM>>("GetAllNotificationsAsAdmin")
                ?? new List<ResultNotificationVM>();

            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var pDict = players.ToDictionary(x => x.Id, x => x);

            foreach (var n in list)
            {
                if (pDict.TryGetValue(n.PlayerId, out var p))
                {
                    n.PlayerDisplay = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})";
                }
            }

            return View(list); // Views/AdminNotification/Notifications.cshtml
        }

        // ============================
        // CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateNotification()
        {
            var vm = new CreateNotificationVM
            {
                Type = "Info"
            };

            await PopulatePlayerOptions(vm);
            PopulateTypeOptions(vm);

            return View(vm); // Views/AdminNotification/CreateNotification.cshtml
        }

        // ============================
        // CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNotification(CreateNotificationVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                PopulateTypeOptions(model);
                return View(model);
            }

            var response = await _notificationClient.PostAsJsonAsync("CreateNotificationAsAdmin", new
            {
                model.PlayerId,
                model.Title,
                model.Message,
                model.Type
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                PopulateTypeOptions(model);
                ModelState.AddModelError(string.Empty, "Notification oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Notifications));
        }

        // ============================
        // EDIT (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditNotification(Guid id)
        {
            var dto = await _notificationClient
                .GetFromJsonAsync<UpdateNotificationVM>($"GetNotificationByIdAsAdmin/{id}");

            if (dto is null) return NotFound();

            await PopulatePlayerOptions(dto);
            PopulateTypeOptions(dto);

            return View(dto); // Views/AdminNotification/EditNotification.cshtml
        }

        // ============================
        // EDIT (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNotification(UpdateNotificationVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                PopulateTypeOptions(model);
                return View(model);
            }

            var response = await _notificationClient.PutAsJsonAsync("UpdateNotificationAsAdmin", new
            {
                model.Id,
                model.PlayerId,
                model.Title,
                model.Message,
                model.Type
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                PopulateTypeOptions(model);
                ModelState.AddModelError(string.Empty, "Notification güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Notifications));
        }

        // ============================
        // DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var response = await _notificationClient.DeleteAsync($"DeleteNotificationAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        // ============================
        // HELPERS
        // ============================
        private async Task PopulatePlayerOptions(CreateNotificationVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = p.Id == model.PlayerId
                })
                .ToList();
        }

        private async Task PopulatePlayerOptions(UpdateNotificationVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = p.Id == model.PlayerId
                })
                .ToList();
        }

        private void PopulateTypeOptions(CreateNotificationVM model)
        {
            model.TypeOptions = new List<SelectListItem>
            {
                new("Info", "Info", model.Type == "Info"),
                new("Success", "Success", model.Type == "Success"),
                new("Warning", "Warning", model.Type == "Warning"),
                new("Error", "Error", model.Type == "Error"),
            };
        }

        private void PopulateTypeOptions(UpdateNotificationVM model)
        {
            model.TypeOptions = new List<SelectListItem>
            {
                new("Info", "Info", model.Type == "Info"),
                new("Success", "Success", model.Type == "Success"),
                new("Warning", "Warning", model.Type == "Warning"),
                new("Error", "Error", model.Type == "Error"),
            };
        }
    }
}
