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
            // Implementation would call a bulk endpoint in Energy API
            var response = await _energyClient.PostAsync("BulkResetEnergy", null);
            if (response.IsSuccessStatusCode)
            {
                SetAlert("Başarılı", "Tüm oyuncuların enerjisi başarıyla fullendi.", "success");
            }
            else
            {
                SetAlert("Hata", "Enerji sıfırlanırken bir hata oluştu.", "error");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearAllCooldowns()
        {
            var response = await _cooldownClient.PostAsync("BulkClearCooldowns", null);
            if (response.IsSuccessStatusCode)
            {
                SetAlert("Başarılı", "Tüm bekleme süreleri başarıyla temizlendi.", "success");
            }
            else
            {
                SetAlert("Hata", "Cooldownlar temizlenirken bir hata oluştu.", "error");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAllPendingUsers()
        {
            var response = await _identityClient.PostAsync("ApproveAllPendingUsers", null);
            if (response.IsSuccessStatusCode)
            {
                SetAlert("Başarılı", "Tüm bekleyen kullanıcı onayları gerçekleştirildi.", "success");
            }
            else
            {
                SetAlert("Hata", "Kullanıcılar onaylanırken bir hata oluştu.", "error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
