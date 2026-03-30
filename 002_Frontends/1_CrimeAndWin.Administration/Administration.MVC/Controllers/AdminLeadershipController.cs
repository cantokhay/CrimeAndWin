using Administration.MVC.ViewModels.LeadershipVMs.LeaderboardEntryVMs;
using Administration.MVC.ViewModels.LeadershipVMs.LeaderboardVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminLeadershipController : Controller
    {
        private readonly HttpClient _leadershipClient;
        private readonly HttpClient _playerClient;
        private readonly HttpClient _gameWorldClient;

        public AdminLeadershipController(IHttpClientFactory httpClientFactory, HttpClient gameWorldClient)
        {
            _leadershipClient = httpClientFactory.CreateClient("LeadershipApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
            _gameWorldClient = httpClientFactory.CreateClient("GameWorldApi"); ;
        }

        #region Leaderboard Operations

        // ============================
        // LEADERBOARD LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Leaderboards()
        {
            var leaderboards = await _leadershipClient
                .GetFromJsonAsync<List<ResultLeaderboardVM>>("GetAllLeaderboardsAsAdmin")
                ?? new List<ResultLeaderboardVM>();

            return View(leaderboards); 
        }

        // ============================
        // LEADERBOARD CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateLeaderboard()
        {
            var vm = new CreateLeaderboardVM
            {
                IsSeasonal = false
            };

            await PopulateGameWorldOptions(vm, vm.GameWorldId);
            await PopulateSeasonOptions(vm, vm.GameWorldId, vm.SeasonId);

            return View(vm);
        }

        // ============================
        // LEADERBOARD CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeaderboard(CreateLeaderboardVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGameWorldOptions(model, model.GameWorldId);
                await PopulateSeasonOptions(model, model.GameWorldId, model.SeasonId);
                return View(model);
            }

            var response = await _leadershipClient.PostAsJsonAsync("CreateLeaderboardAsAdmin", new
            {
                model.Name,
                model.Description,
                model.GameWorldId,
                model.SeasonId,
                model.IsSeasonal
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateGameWorldOptions(model, model.GameWorldId);
                await PopulateSeasonOptions(model, model.GameWorldId, model.SeasonId);
                ModelState.AddModelError(string.Empty, "Leaderboard oluşturulurken hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Leaderboards));
        }

        // ============================
        // LEADERBOARD UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditLeaderboard(Guid id)
        {
            var dto = await _leadershipClient
                .GetFromJsonAsync<UpdateLeaderboardVM>($"GetLeaderboardByIdAsAdmin/{id}");

            if (dto is null)
                return NotFound();

            await PopulateGameWorldOptions(dto, dto.GameWorldId);
            await PopulateSeasonOptions(dto, dto.GameWorldId, dto.SeasonId);

            return View(dto);
        }

        // ============================
        // LEADERBOARD UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLeaderboard(UpdateLeaderboardVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGameWorldOptions(model, model.GameWorldId);
                await PopulateSeasonOptions(model, model.GameWorldId, model.SeasonId);
                return View(model);
            }

            var response = await _leadershipClient.PutAsJsonAsync("UpdateLeaderboardAsAdmin", new
            {
                model.Id,
                model.Name,
                model.Description,
                model.GameWorldId,
                model.SeasonId,
                model.IsSeasonal
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateGameWorldOptions(model, model.GameWorldId);
                await PopulateSeasonOptions(model, model.GameWorldId, model.SeasonId);
                ModelState.AddModelError(string.Empty, "Leaderboard güncellenirken hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Leaderboards));
        }

        // ============================
        // LEADERBOARD DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeaderboard(Guid id)
        {
            var response = await _leadershipClient.DeleteAsync($"DeleteLeaderboardAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        // ============================
        // GET SEASONS BY GAMEWORLD
        // ============================
        [HttpGet]
        public async Task<IActionResult> GetSeasonsByGameWorld(Guid gameWorldId)
        {
            var seasons = await _gameWorldClient
                .GetFromJsonAsync<List<SeasonProxy>>("Seasons/GetAll")
                ?? new List<SeasonProxy>();

            var list = seasons
                .Where(s => s.GameWorldId == gameWorldId)
                .OrderByDescending(s => s.IsActive)
                .ThenByDescending(s => s.SeasonNumber)
                .Select(s => new { id = s.Id, text = $"Season {s.SeasonNumber}" + (s.IsActive ? " (Active)" : "") })
                .ToList();

            return Json(list);
        }

        #endregion

        #region LeaderboardEntry Operations

        // ============================
        // ENTRY LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Entries()
        {
            var entries = await _leadershipClient
                .GetFromJsonAsync<List<ResultLeaderboardEntryVM>>("GetAllLeaderboardEntriesAsAdmin")
                ?? new List<ResultLeaderboardEntryVM>();

            // Leaderboard display
            var leaderboards = await _leadershipClient
                .GetFromJsonAsync<List<ResultLeaderboardVM>>("GetAllLeaderboardsAsAdmin")
                ?? new List<ResultLeaderboardVM>();

            var lbDict = leaderboards.ToDictionary(x => x.Id, x => x);

            // Player display
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var pDict = players.ToDictionary(x => x.Id, x => x);

            foreach (var e in entries)
            {
                if (lbDict.TryGetValue(e.LeaderboardId, out var lb))
                    e.LeaderboardDisplay = $"{lb.Name} ({lb.Id})";

                if (pDict.TryGetValue(e.PlayerId, out var p))
                    e.PlayerDisplay = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})";
            }

            return View(entries); // Views/AdminLeadership/Entries.cshtml
        }

        // ============================
        // ENTRY CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateEntry()
        {
            var vm = new CreateLeaderboardEntryVM
            {
                IsActive = true
            };

            await PopulateLeaderboardOptions(vm);
            await PopulatePlayerOptions(vm);

            return View(vm); // Views/AdminLeadership/CreateEntry.cshtml
        }

        // ============================
        // ENTRY CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntry(CreateLeaderboardEntryVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateLeaderboardOptions(model);
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _leadershipClient.PostAsJsonAsync("CreateLeaderboardEntryAsAdmin", new
            {
                model.LeaderboardId,
                model.PlayerId,
                model.RankPoints,
                model.Position,
                model.IsActive
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateLeaderboardOptions(model);
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Leaderboard entry oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Entries));
        }

        // ============================
        // ENTRY UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditEntry(Guid id)
        {
            var res = await _leadershipClient.GetAsync($"GetLeaderboardEntryByIdAsAdmin/{id}");

            if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            if (!res.IsSuccessStatusCode)
                return StatusCode((int)res.StatusCode, "Leadership API error");

            var dto = await res.Content.ReadFromJsonAsync<ResultLeaderboardEntryVM>();
            if (dto is null) return NotFound();

            var vm = new UpdateLeaderboardEntryVM
            {
                Id = dto.Id,
                LeaderboardId = dto.LeaderboardId,
                PlayerId = dto.PlayerId,
                RankPoints = dto.RankPoints,
                Position = dto.Position,
                IsActive = dto.IsActive
            };

            await PopulateLeaderboardOptions(vm);
            await PopulatePlayerOptions(vm);

            return View(vm); // Views/AdminLeadership/EditEntry.cshtml
        }

        // ============================
        // ENTRY UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEntry(UpdateLeaderboardEntryVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateLeaderboardOptions(model);
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _leadershipClient.PutAsJsonAsync("UpdateLeaderboardEntryAsAdmin", new
            {
                model.Id,
                model.LeaderboardId,
                model.PlayerId,
                model.RankPoints,
                model.Position,
                model.IsActive
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateLeaderboardOptions(model);
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Leaderboard entry güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Entries));
        }

        // ============================
        // ENTRY DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            var response = await _leadershipClient.DeleteAsync($"DeleteLeaderboardEntryAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        #endregion

        #region Helpers

        private async Task PopulatePlayerOptions(CreateLeaderboardEntryVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                Selected = (p.Id == model.PlayerId)
            }).ToList();
        }

        private async Task PopulatePlayerOptions(UpdateLeaderboardEntryVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                Selected = (p.Id == model.PlayerId)
            }).ToList();
        }

        private async Task PopulateLeaderboardOptions(CreateLeaderboardEntryVM model)
        {
            var lbs = await _leadershipClient
                .GetFromJsonAsync<List<ResultLeaderboardVM>>("GetAllLeaderboardsAsAdmin")
                ?? new List<ResultLeaderboardVM>();

            model.LeaderboardOptions = lbs.Select(lb => new SelectListItem
            {
                Value = lb.Id.ToString(),
                Text = $"{lb.Name} ({lb.Id})",
                Selected = (lb.Id == model.LeaderboardId)
            }).ToList();
        }

        private async Task PopulateLeaderboardOptions(UpdateLeaderboardEntryVM model)
        {
            var lbs = await _leadershipClient
                .GetFromJsonAsync<List<ResultLeaderboardVM>>("GetAllLeaderboardsAsAdmin")
                ?? new List<ResultLeaderboardVM>();

            model.LeaderboardOptions = lbs.Select(lb => new SelectListItem
            {
                Value = lb.Id.ToString(),
                Text = $"{lb.Name} ({lb.Id})",
                Selected = (lb.Id == model.LeaderboardId)
            }).ToList();
        }

        private async Task PopulateGameWorldOptions(dynamic model, Guid? selectedGameWorldId)
        {
            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<GameWorldProxy>>("GameWorlds")
                ?? new List<GameWorldProxy>();

            model.GameWorldOptions = gameWorlds
                .Select(gw => new SelectListItem
                {
                    Value = gw.Id.ToString(),
                    Text = gw.Name,
                    Selected = selectedGameWorldId.HasValue && gw.Id == selectedGameWorldId.Value
                })
                .ToList();
        }

        private async Task PopulateSeasonOptions(dynamic model, Guid? gameWorldId, Guid? selectedSeasonId)
        {
            var seasons = await _gameWorldClient
                .GetFromJsonAsync<List<SeasonProxy>>("Seasons/GetAll")
                ?? new List<SeasonProxy>();

            if (gameWorldId.HasValue)
                seasons = seasons.Where(s => s.GameWorldId == gameWorldId.Value).ToList();

            model.SeasonOptions = seasons
                .OrderByDescending(s => s.IsActive)
                .ThenByDescending(s => s.SeasonNumber)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Season {s.SeasonNumber}" + (s.IsActive ? " (Active)" : ""),
                    Selected = selectedSeasonId.HasValue && s.Id == selectedSeasonId.Value
                })
                .ToList();
        }

        private sealed class GameWorldProxy
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
        }

        private sealed class SeasonProxy
        {
            public Guid Id { get; set; }
            public Guid GameWorldId { get; set; }
            public int SeasonNumber { get; set; }
            public bool IsActive { get; set; }
        }

        #endregion
    }
}
