using Administration.MVC.ViewModels.GameWorldVMs.GameWorldVMs;
using Administration.MVC.ViewModels.GameWorldVMs.SeasonVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminGameWorldController : Controller
    {
        private readonly HttpClient _gameWorldClient;

        public AdminGameWorldController(IHttpClientFactory httpClientFactory)
        {
            _gameWorldClient = httpClientFactory.CreateClient("GameWorldApi");
        }

        // ============================
        // GAMEWORLD LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> GameWorlds()
        {
            var result = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds");

            var model = result ?? new List<ResultGameWorldVM>();

            return View(model);
        }

        // ============================
        // GAMEWORLD CREATE (GET)
        // ============================
        [HttpGet]
        public IActionResult CreateGameWorld()
        {
            var vm = new CreateGameWorldVM
            {
                MaxEnergy = 100,
                RegenRatePerHour = 10
            };

            return View(vm);
        }

        // ============================
        // GAMEWORLD CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGameWorld(CreateGameWorldVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _gameWorldClient.PostAsJsonAsync("GameWorlds", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "GameWorld oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(GameWorlds));
        }

        // ============================
        // GAMEWORLD EDIT (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditGameWorld(Guid id)
        {
            var model = await _gameWorldClient
                .GetFromJsonAsync<UpdateGameWorldVM>($"GameWorlds/{id}");

            if (model is null)
            {
                return NotFound();
            }

            model.Seasons ??= new List<ResultSeasonVM>();

            model.Seasons = model.Seasons
                .OrderBy(x => x.SeasonNumber)
                .ToList();

            foreach (var season in model.Seasons)
            {
                season.GameWorldId = model.Id;
                season.GameWorldName = model.Name;
            }

            return View(model);
        }

        // ============================
        // GAMEWORLD EDIT (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGameWorld(UpdateGameWorldVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGameWorldSeasons(model);
                return View(model);
            }

            var payload = new
            {
                GameWorldId = model.Id,
                model.Name,
                model.MaxEnergy,
                model.RegenRatePerHour
            };

            var response = await _gameWorldClient
                .PutAsJsonAsync($"GameWorlds/{model.Id}/GameWorld", payload);

            if (!response.IsSuccessStatusCode)
            {
                await PopulateGameWorldSeasons(model);
                ModelState.AddModelError(string.Empty, "GameWorld güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(GameWorlds));
        }

        // -----------------------
        // GAME WORLD DELETE
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGameWorld(Guid id)
        {
            var response = await _gameWorldClient.DeleteAsync($"GameWorlds/DeleteGameWorld/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        // ============================
        // SEASON LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Seasons()
        {
            var seasons = await _gameWorldClient
                .GetFromJsonAsync<List<ResultSeasonVM>>("Seasons/GetAll")
                ?? new List<ResultSeasonVM>();

            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds")
                ?? new List<ResultGameWorldVM>();

            var model = new List<ResultSeasonVM>();

            foreach (var gameWorld in gameWorlds)
            {
                if (gameWorld.Seasons is null || !gameWorld.Seasons.Any())
                {
                    continue;
                }

                foreach (var season in gameWorld.Seasons)
                {
                    var seasonFromApi = seasons.FirstOrDefault(x => x.Id == season.Id);

                    if (seasonFromApi is null)
                    {
                        season.GameWorldId = gameWorld.Id;
                        season.GameWorldName = gameWorld.Name;
                        model.Add(season);
                        continue;
                    }

                    seasonFromApi.GameWorldId = gameWorld.Id;
                    seasonFromApi.GameWorldName = gameWorld.Name;
                    model.Add(seasonFromApi);
                }
            }

            model = model
                .OrderBy(x => x.GameWorldName)
                .ThenBy(x => x.SeasonNumber)
                .ToList();

            return View(model);
        }

        // ============================
        // SEASON CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateSeason(Guid? gameWorldId = null, string? returnUrl = null)
        {
            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds")
                ?? new List<ResultGameWorldVM>();

            var vm = new CreateSeasonVM
            {
                GameWorldId = gameWorldId ?? Guid.Empty,
                StartUtc = DateTime.UtcNow,
                EndUtc = DateTime.UtcNow.AddDays(7),
                IsActive = true,
                ReturnUrl = returnUrl,
                GameWorldOptions = gameWorlds
                    .Select(gw => new SelectListItem
                    {
                        Value = gw.Id.ToString(),
                        Text = $"{gw.Name} (MaxEnergy: {gw.MaxEnergy})",
                        Selected = gameWorldId.HasValue && gw.Id == gameWorldId.Value
                    })
                    .ToList()
            };

            return View(vm);
        }

        // ============================
        // SEASON CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSeason(CreateSeasonVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGameWorldOptions(model);
                return View(model);
            }

            var payload = new
            {
                GameWorldId = model.GameWorldId,
                model.SeasonNumber,
                model.StartUtc,
                model.EndUtc,
                model.IsActive
            };

            var response = await _gameWorldClient
                .PostAsJsonAsync($"Seasons/{model.GameWorldId}/seasons", payload);

            if (!response.IsSuccessStatusCode)
            {
                await PopulateGameWorldOptions(model);
                ModelState.AddModelError(string.Empty, "Season oluşturulurken bir hata oluştu.");
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Seasons));
        }

        // ============================
        // SEASON EDIT (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditSeason(Guid id, string? returnUrl = null)
        {
            var season = await _gameWorldClient
                .GetFromJsonAsync<UpdateSeasonVM>($"Seasons/{id}");

            if (season is null)
            {
                return NotFound();
            }

            season.ReturnUrl = returnUrl;

            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds")
                ?? new List<ResultGameWorldVM>();

            var ownerGameWorld = gameWorlds
                .FirstOrDefault(gw => gw.Seasons != null && gw.Seasons.Any(s => s.Id == season.Id));

            if (ownerGameWorld is not null)
            {
                season.GameWorldId = ownerGameWorld.Id;
            }

            await PopulateGameWorldOptions(season);

            return View(season);
        }

        // ============================
        // SEASON EDIT (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeason(UpdateSeasonVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateGameWorldOptions(model);
                return View(model);
            }

            var payload = new
            {
                SeasonId = model.Id,
                model.GameWorldId,
                model.SeasonNumber,
                model.StartUtc,
                model.EndUtc,
                model.IsActive
            };

            var response = await _gameWorldClient
                .PutAsJsonAsync($"Seasons/{model.Id}", payload);

            if (!response.IsSuccessStatusCode)
            {
                await PopulateGameWorldOptions(model);
                ModelState.AddModelError(string.Empty, "Season güncellenirken bir hata oluştu.");
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Seasons));
        }

        // -----------------------
        // SEASON DELETE
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSeason(Guid id)
        {
            var response = await _gameWorldClient.DeleteAsync($"Seasons/DeleteSeason/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        // ============================
        // HELPERS
        // ============================
        private async Task PopulateGameWorldOptions(CreateSeasonVM model)
        {
            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds")
                ?? new List<ResultGameWorldVM>();

            model.GameWorldOptions = gameWorlds
                .Select(gw => new SelectListItem
                {
                    Value = gw.Id.ToString(),
                    Text = $"{gw.Name} (MaxEnergy: {gw.MaxEnergy})",
                    Selected = gw.Id == model.GameWorldId
                })
                .ToList();
        }

        private async Task PopulateGameWorldOptions(UpdateSeasonVM model)
        {
            var gameWorlds = await _gameWorldClient
                .GetFromJsonAsync<List<ResultGameWorldVM>>("GameWorlds")
                ?? new List<ResultGameWorldVM>();

            model.GameWorldOptions = gameWorlds
                .Select(gw => new SelectListItem
                {
                    Value = gw.Id.ToString(),
                    Text = $"{gw.Name} (MaxEnergy: {gw.MaxEnergy})",
                    Selected = gw.Id == model.GameWorldId
                })
                .ToList();
        }

        private async Task PopulateGameWorldSeasons(UpdateGameWorldVM model)
        {
            var existing = await _gameWorldClient
                .GetFromJsonAsync<UpdateGameWorldVM>($"GameWorlds/{model.Id}");

            model.Seasons = existing?.Seasons?.ToList() ?? new List<ResultSeasonVM>();

            model.Seasons = model.Seasons
                .OrderBy(x => x.SeasonNumber)
                .ToList();

            foreach (var season in model.Seasons)
            {
                season.GameWorldId = model.Id;
                season.GameWorldName = model.Name;
            }
        }
    }
}