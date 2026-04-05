using Administration.MVC.ViewModels.SettingsVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminSettingsController : BaseAdminController
    {
        private readonly HttpClient _systemClient;

        public AdminSettingsController(IHttpClientFactory httpClientFactory)
        {
            _systemClient = httpClientFactory.CreateClient("SystemApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _systemClient
                .GetFromJsonAsync<GlobalSettingsVM>("GetGlobalSettings")
                ?? new GlobalSettingsVM();

            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings(GlobalSettingsVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var response = await _systemClient.PostAsJsonAsync("UpdateGlobalSettings", model);

            if (response.IsSuccessStatusCode)
            {
                SetTempData("success", "Başarılı", "Sistem ayarları başarıyla güncellendi ve tüm servislere yayılması için kuyruğa alındı.");
            }
            else
            {
                SetTempData("error", "Hata", "Ayarlar güncellenirken bir hata oluştu.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
