using Administration.MVC.ViewModels.IdentityVMs.AppUserVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.PlayerVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminPlayerProfileController : Controller
    {
        private readonly HttpClient _playerClient;
        private readonly HttpClient _identityClient;

        public AdminPlayerProfileController(IHttpClientFactory httpClientFactory)
        {
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
            _identityClient = httpClientFactory.CreateClient("IdentityApi");
        }

        // ============================
        // PLAYER LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Players()
        {
            // 1) Player list
            var players = await _playerClient
                .GetFromJsonAsync<List<ResultPlayerVM>>("GetAllPlayersAsAdmin")
                ?? new List<ResultPlayerVM>();

            // 2) AppUser list (Identity’den)
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var userDict = users.ToDictionary(x => x.Id, x => x);

            // 3) UI için username/email doldur
            foreach (var player in players)
            {
                if (userDict.TryGetValue(player.AppUserId, out var appUser))
                {
                    player.AppUserName = appUser.UserName;
                    player.AppUserEmail = appUser.Email;
                }
            }

            return View(players); // Views/AdminPlayerProfile/Players.cshtml
        }

        // ============================
        // PLAYER CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreatePlayer()
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var vm = new CreatePlayerVM
            {
                // Varsayılan değerler (istersen değiştir)
                Power = 10,
                Defense = 10,
                Agility = 10,
                Luck = 10,
                EnergyCurrent = 100,
                EnergyMax = 100,
                EnergyRegenPerMinute = 1,
                RankPoints = 0,
                RankPosition = null,
                LastEnergyCalcUtc = DateTime.UtcNow,
                UserOptions = users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = $"{u.UserName} ({u.Email})"
                    })
                    .ToList()
            };

            return View(vm); // Views/AdminPlayerProfile/CreatePlayer.cshtml
        }

        // ============================
        // PLAYER CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlayer(CreatePlayerVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerUserOptions(model);
                return View(model);
            }

            // LastEnergyCalcUtc set edilmemişse bir default verelim
            if (model.LastEnergyCalcUtc == default)
            {
                model.LastEnergyCalcUtc = DateTime.UtcNow;
            }

            var response = await _playerClient.PostAsJsonAsync("CreatePlayerAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerUserOptions(model);
                ModelState.AddModelError(string.Empty, "Player oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Players));
        }

        // ============================
        // PLAYER UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditPlayer(Guid id)
        {
            var dto = await _playerClient
                .GetFromJsonAsync<UpdatePlayerVM>($"GetByIdPlayerAsAdmin/{id}");

            if (dto is null)
                return NotFound();

            await PopulatePlayerUserOptions(dto);

            return View(dto); // Views/AdminPlayerProfile/EditPlayer.cshtml
        }

        // ============================
        // PLAYER UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlayer(UpdatePlayerVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerUserOptions(model);
                return View(model);
            }

            if (model.LastEnergyCalcUtc == default)
            {
                model.LastEnergyCalcUtc = DateTime.UtcNow;
            }

            var response = await _playerClient.PutAsJsonAsync("UpdatePlayerAsAdmin", model);

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerUserOptions(model);
                ModelState.AddModelError(string.Empty, "Player güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Players));
        }

        // ============================
        // PLAYER DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var response = await _playerClient.DeleteAsync($"DeletePlayerAsAdmin/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        // ============================
        // Helper: User dropdown doldur
        // ============================
        private async Task PopulatePlayerUserOptions(CreatePlayerVM model)
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            model.UserOptions = users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.UserName} ({u.Email})",
                    Selected = (u.Id == model.AppUserId)
                })
                .ToList();
        }

        private async Task PopulatePlayerUserOptions(UpdatePlayerVM model)
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            model.UserOptions = users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.UserName} ({u.Email})",
                    Selected = (u.Id == model.AppUserId)
                })
                .ToList();
        }
    }
}
