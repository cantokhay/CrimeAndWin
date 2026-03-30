using Administration.MVC.ViewModels.ModerationVMs.ActionVMs;
using Administration.MVC.ViewModels.ModerationVMs.ReportVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminModerationController : Controller
    {
        private readonly HttpClient _moderationClient;
        private readonly HttpClient _playerClient;

        public AdminModerationController(IHttpClientFactory httpClientFactory)
        {
            _moderationClient = httpClientFactory.CreateClient("ModerationApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
        }

        // =========================================
        // REPORTS
        // =========================================
        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            var list = await _moderationClient
                .GetFromJsonAsync<List<ResultReportVM>>("GetAllReportsAsAdmin")
                ?? new List<ResultReportVM>();

            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var dict = players.ToDictionary(x => x.Id, x => x);

            foreach (var r in list)
            {
                if (dict.TryGetValue(r.ReporterId, out var rep))
                    r.ReporterDisplay = $"{(string.IsNullOrWhiteSpace(rep.DisplayName) ? "Player" : rep.DisplayName)} ({rep.Id})";

                if (dict.TryGetValue(r.ReportedPlayerId, out var rp))
                    r.ReportedPlayerDisplay = $"{(string.IsNullOrWhiteSpace(rp.DisplayName) ? "Player" : rp.DisplayName)} ({rp.Id})";
            }

            return View(list); // Views/AdminModeration/Reports.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> CreateReport()
        {
            var vm = new CreateReportVM();
            await PopulatePlayerOptions(vm.PlayerOptions);
            return View(vm); // Views/AdminModeration/CreateReport.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(CreateReportVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                return View(model);
            }

            var response = await _moderationClient.PostAsJsonAsync("CreateReportAsAdmin", new
            {
                model.ReporterId,
                model.ReportedPlayerId,
                model.Reason,
                model.Description,
                model.IsResolved,
                model.ResolvedAtUtc,
                model.ResolvedByModeratorId
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                ModelState.AddModelError(string.Empty, "Report oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Reports));
        }

        [HttpGet]
        public async Task<IActionResult> EditReport(Guid id)
        {
            var dto = await _moderationClient
                .GetFromJsonAsync<UpdateReportVM>($"GetReportByIdAsAdmin/{id}");

            if (dto is null) return NotFound();

            await PopulatePlayerOptions(dto.PlayerOptions);
            return View(dto); // Views/AdminModeration/EditReport.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReport(UpdateReportVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                return View(model);
            }

            var response = await _moderationClient.PutAsJsonAsync("UpdateReportAsAdmin", new
            {
                model.Id,
                model.ReporterId,
                model.ReportedPlayerId,
                model.Reason,
                model.Description,
                model.IsResolved,
                model.ResolvedAtUtc,
                model.ResolvedByModeratorId
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                ModelState.AddModelError(string.Empty, "Report güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Reports));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var response = await _moderationClient.DeleteAsync($"DeleteReportAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        // =========================================
        // MODERATION ACTIONS
        // =========================================
        [HttpGet]
        public async Task<IActionResult> ModerationActions()
        {
            var list = await _moderationClient
                .GetFromJsonAsync<List<ResultModerationActionVM>>("GetAllModerationActionsAsAdmin")
                ?? new List<ResultModerationActionVM>();

            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var dict = players.ToDictionary(x => x.Id, x => x);

            foreach (var a in list)
            {
                if (dict.TryGetValue(a.PlayerId, out var p))
                    a.PlayerDisplay = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})";
            }

            return View(list); // Views/AdminModeration/ModerationActions.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> CreateModerationAction()
        {
            var vm = new CreateModerationActionVM();
            await PopulatePlayerOptions(vm.PlayerOptions);
            PopulateActionTypeOptions(vm.ActionTypeOptions, vm.ActionType);

            return View(vm); // Views/AdminModeration/CreateModerationAction.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModerationAction(CreateModerationActionVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                PopulateActionTypeOptions(model.ActionTypeOptions, model.ActionType);
                return View(model);
            }

            var response = await _moderationClient.PostAsJsonAsync("CreateModerationActionAsAdmin", new
            {
                model.PlayerId,
                model.ActionType,
                model.Reason,
                model.ActionDateUtc,
                model.ExpiryDateUtc,
                model.ModeratorId,
                model.IsActive
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                PopulateActionTypeOptions(model.ActionTypeOptions, model.ActionType);
                ModelState.AddModelError(string.Empty, "ModerationAction oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(ModerationActions));
        }

        [HttpGet]
        public async Task<IActionResult> EditModerationAction(Guid id)
        {
            var dto = await _moderationClient
                .GetFromJsonAsync<UpdateModerationActionVM>($"GetModerationActionByIdAsAdmin/{id}");

            if (dto is null) return NotFound();

            await PopulatePlayerOptions(dto.PlayerOptions);
            PopulateActionTypeOptions(dto.ActionTypeOptions, dto.ActionType);

            return View(dto); // Views/AdminModeration/EditModerationAction.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModerationAction(UpdateModerationActionVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                PopulateActionTypeOptions(model.ActionTypeOptions, model.ActionType);
                return View(model);
            }

            var response = await _moderationClient.PutAsJsonAsync("UpdateModerationActionAsAdmin", new
            {
                model.Id,
                model.PlayerId,
                model.ActionType,
                model.Reason,
                model.ActionDateUtc,
                model.ExpiryDateUtc,
                model.ModeratorId,
                model.IsActive
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model.PlayerOptions);
                PopulateActionTypeOptions(model.ActionTypeOptions, model.ActionType);
                ModelState.AddModelError(string.Empty, "ModerationAction güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(ModerationActions));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModerationAction(Guid id)
        {
            var response = await _moderationClient.DeleteAsync($"DeleteModerationActionAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        // =========================================
        // HELPERS
        // =========================================
        private async Task PopulatePlayerOptions(List<SelectListItem> target)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            target.Clear();
            target.AddRange(players.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})"
            }));
        }

        private void PopulateActionTypeOptions(List<SelectListItem> target, string? selected)
        {
            target.Clear();
            target.Add(new SelectListItem("Ban", "Ban", (selected == "Ban")));
            target.Add(new SelectListItem("Mute", "Mute", (selected == "Mute")));
            target.Add(new SelectListItem("Warning", "Warning", (selected == "Warning")));
            target.Add(new SelectListItem("Suspend", "Suspend", (selected == "Suspend")));
        }
    }
}
