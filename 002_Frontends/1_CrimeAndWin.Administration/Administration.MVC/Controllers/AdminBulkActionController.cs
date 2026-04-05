using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminBulkActionController : BaseAdminController
    {
        private readonly HttpClient _identityClient;
        private readonly HttpClient _playerClient;
        private readonly HttpClient _energyClient;
        private readonly HttpClient _cooldownClient;
        private readonly HttpClient _actionLogClient;

        public AdminBulkActionController(IHttpClientFactory httpClientFactory)
        {
            _identityClient = httpClientFactory.CreateClient("IdentityApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
            _energyClient = httpClientFactory.CreateClient("EnergyApi");
            _cooldownClient = httpClientFactory.CreateClient("CooldownApi");
            _actionLogClient = httpClientFactory.CreateClient("ActionLogApi");
        }

        public IActionResult Index()
        {
            return View(); // Views/AdminBulkAction/Index.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAllEnergy()
        {
            var response = await _energyClient.PostAsync("BulkResetEnergy", null);
            return Json(new { 
                success = response.IsSuccessStatusCode, 
                message = response.IsSuccessStatusCode ? "Tüm oyuncuların enerjisi başarıyla fullendi." : "Enerji sıfırlanırken bir hata oluştu." 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearAllCooldowns()
        {
            var response = await _cooldownClient.PostAsync("BulkClearCooldowns", null);
            return Json(new { 
                success = response.IsSuccessStatusCode, 
                message = response.IsSuccessStatusCode ? "Tüm bekleme süreleri başarıyla temizlendi." : "Cooldownlar temizlenirken bir hata oluştu." 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAllPendingUsers()
        {
            var response = await _identityClient.PostAsync("ApproveAllPendingUsers", null);
            return Json(new { 
                success = response.IsSuccessStatusCode, 
                message = response.IsSuccessStatusCode ? "Tüm bekleyen kullanıcı onayları gerçekleştirildi." : "Kullanıcılar onaylanırken bir hata oluştu." 
            });
        }

    }
}
