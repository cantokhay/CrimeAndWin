using Administration.MVC.ViewModels.SearchVMs;
using Administration.MVC.ViewModels.IdentityVMs.AppUserVMs;
using Administration.MVC.ViewModels.ActionViewModels;
using Administration.MVC.ViewModels.GameWorldVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminSearchController : BaseAdminController
    {
        private readonly HttpClient _identityClient;
        private readonly HttpClient _playerClient;
        private readonly HttpClient _actionClient;
        private readonly HttpClient _gameWorldClient;

        public AdminSearchController(IHttpClientFactory httpClientFactory)
        {
            _identityClient = httpClientFactory.CreateClient("IdentityApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
            _actionClient = httpClientFactory.CreateClient("ActionApi");
            _gameWorldClient = httpClientFactory.CreateClient("GameWorldApi");
        }

        [HttpGet]
        public async Task<IActionResult> GlobalSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length < 2)
            {
                return Json(new { success = true, results = new List<GlobalSearchResultVM>() });
            }

            var query = q.ToLowerInvariant();
            var results = new List<GlobalSearchResultVM>();

            try
            {
                // Parallel Search across services
                var usersTask = _identityClient.GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers");
                var playersTask = _playerClient.GetFromJsonAsync<List<ResultPlayerVM>>("GetAllPlayers");
                var actionsTask = _actionClient.GetFromJsonAsync<List<ResultActionDefinitionVM>>("GetAllActionDefinitions");
                var worldsTask = _gameWorldClient.GetFromJsonAsync<List<ResultGameWorldVM>>("GetAllGameWorlds");

                await Task.WhenAll(usersTask, playersTask, actionsTask, worldsTask);

                // 1. Users
                if (usersTask.Result != null)
                {
                    var foundUsers = usersTask.Result
                        .Where(x => x.UserName.ToLowerInvariant().Contains(query) || x.Email.ToLowerInvariant().Contains(query))
                        .Take(5)
                        .Select(x => new GlobalSearchResultVM
                        {
                            Title = x.UserName,
                            Description = x.Email,
                            Category = "Kullanıcı",
                            Icon = "user",
                            BadgeColor = "primary",
                            Url = Url.Action("EditUser", "AdminIdentity", new { id = x.Id })
                        });
                    results.AddRange(foundUsers);
                }

                // 2. Players
                if (playersTask.Result != null)
                {
                    var foundPlayers = playersTask.Result
                        .Where(x => x.DisplayName.ToLowerInvariant().Contains(query))
                        .Take(5)
                        .Select(x => new GlobalSearchResultVM
                        {
                            Title = x.DisplayName,
                            Description = $"Güç: {x.Power}, Rank: {x.RankPoints}",
                            Category = "Oyuncu",
                            Icon = "profile-user",
                            BadgeColor = "success",
                            Url = Url.Action("Players", "AdminPlayerProfile") // Typically filters would be better but let's point to list for now
                        });
                    results.AddRange(foundPlayers);
                }

                // 3. Actions
                if (actionsTask.Result != null)
                {
                    var foundActions = actionsTask.Result
                        .Where(x => x.Name.ToLowerInvariant().Contains(query))
                        .Take(5)
                        .Select(x => new GlobalSearchResultVM
                        {
                            Title = x.Name,
                            Description = $"Enerji: {x.EnergyCost}, Cooldown: {x.CooldownSeconds}s",
                            Category = "Aksiyon",
                            Icon = "abstract-26",
                            BadgeColor = "info",
                            Url = Url.Action("ActionDefinitions", "AdminAction")
                        });
                    results.AddRange(foundActions);
                }

                // 4. Worlds
                if (worldsTask.Result != null)
                {
                    var foundWorlds = worldsTask.Result
                        .Where(x => x.WorldName.ToLowerInvariant().Contains(query))
                        .Take(5)
                        .Select(x => new GlobalSearchResultVM
                        {
                            Title = x.WorldName,
                            Description = $"Sezon: {x.CurrentSeasonNumber}",
                            Category = "Dünya",
                            Icon = "global",
                            BadgeColor = "warning",
                            Url = Url.Action("GameWorlds", "AdminGameWorld")
                        });
                    results.AddRange(foundWorlds);
                }
            }
            catch (Exception ex)
            {
                // Silent fail for search for now, just return what we have or empty
            }

            return Json(new { success = true, results = results });
        }
    }
}
