using Administration.MVC.Services.Dtos;
using Administration.MVC.ViewModels.DashboardVMs;
using Administration.MVC.ViewModels.EconomyVMs.WalletVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.PlayerVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminDashboardController : BaseAdminController
    {
        private readonly HttpClient _identityClient;
        private readonly HttpClient _playerClient;
        private readonly HttpClient _economyClient;
        private readonly HttpClient _actionLogClient;
        private readonly HttpClient _healthClient;
        private readonly HttpClient _sagaClient;

        public AdminDashboardController(IHttpClientFactory httpClientFactory)
        {
            _identityClient = httpClientFactory.CreateClient("IdentityApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
            _economyClient = httpClientFactory.CreateClient("EconomyApi");
            _actionLogClient = httpClientFactory.CreateClient("ActionLogApi");
            _healthClient = httpClientFactory.CreateClient("HealthApi");
            _sagaClient = httpClientFactory.CreateClient("SagaApi");
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardVM();

            try
            {
                // Parallel fetching of dashboard data
                // HealthApi base: api/action/admin/
                var healthTask = _healthClient.GetFromJsonAsync<List<ServiceHealthDto>>("GetServiceHealth");
                
                // PlayerProfileApi base: api/PlayerAdmins/
                var playersTask = _playerClient.GetFromJsonAsync<List<ResultPlayerVM>>("GetAllPlayersAsAdmin");
                
                // EconomyApi base: api/EconomyAdmins/
                var walletsTask = _economyClient.GetFromJsonAsync<List<ResultWalletVM>>("GetAllWalletsAsAdmin");
                
                // ActionLogApi base: api/action/admin/
                var actionLogsTask = _actionLogClient.GetFromJsonAsync<List<ActionLogDto>>("logs");
                
                // SagaApi base: api/saga/admin/
                var sagasTask = _sagaClient.GetFromJsonAsync<List<SagaStateDto>>("states");

                await Task.WhenAll(healthTask, playersTask, walletsTask, actionLogsTask, sagasTask);

                // 1. Health
                if (healthTask.Result != null)
                {
                    model.HealthyServiceCount = healthTask.Result.Count(x => x.IsHealthy);
                    model.TotalServiceCount = healthTask.Result.Count;
                }

                // 2. Players
                if (playersTask.Result != null)
                {
                    model.TotalPlayerCount = playersTask.Result.Count;
                    model.NewPlayersToday = playersTask.Result.Count(x => x.CreatedAtUtc >= DateTime.Today);
                }

                // 3. Economy
                if (walletsTask.Result != null)
                {
                    model.TotalCashInEconomy = walletsTask.Result.Sum(x => x.Balance);
                }

                // 4. Action Logs & Trends
                if (actionLogsTask.Result != null)
                {
                    var todayActions = actionLogsTask.Result.Where(x => x.ActionAt >= DateTime.Today).ToList();
                    model.TotalActionsToday = todayActions.Count;
                    model.AverageActionSuccessRate = todayActions.Any() 
                        ? (double)todayActions.Count(x => x.IsSuccess) / todayActions.Count * 100 
                        : 0;

                    model.RecentActions = actionLogsTask.Result
                        .OrderByDescending(x => x.ActionAt)
                        .Take(10)
                        .Select(x => new RecentActionLogVM
                        {
                            ActionName = x.ActionType,
                            PlayerName = !string.IsNullOrEmpty(x.PlayerName) ? x.PlayerName : x.PlayerId.ToString().Substring(0, 8),
                            IsSuccess = x.IsSuccess,
                            Timestamp = x.ActionAt
                        }).ToList();
                }

                // 5. Saga Failures
                if (sagasTask.Result != null)
                {
                    model.RecentFailures = sagasTask.Result
                        .Where(x => x.CurrentState == "Failed")
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(5)
                        .Select(x => new RecentSagaFailureVM
                        {
                            SagaType = x.SagaType,
                            ErrorMessage = x.FailReason,
                            Timestamp = x.CreatedAt
                        }).ToList();
                }

                // Mocking Chart Data for now (In a real app, this would be grouped results from the API)
                model.ActionVolumeChartData = "[44, 55, 57, 56, 61, 58, 63, 60, 66]";
                model.SuccessRateChartData = "[76, 85, 101, 98, 87, 105, 91, 114, 94]";
            }
            catch (Exception ex)
            {
                // Dashboard should be resilient
            }

            return View(model);
        }
    }
}
