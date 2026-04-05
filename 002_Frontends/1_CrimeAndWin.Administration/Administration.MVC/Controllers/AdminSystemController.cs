using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminSystemController : BaseAdminController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminSystemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Seed()
        {
            return View("Seeding");
        }

        [HttpPost]
        public async Task<IActionResult> RunSeed(string serviceName, string endpoint, int count)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(serviceName);
                var response = await client.PostAsync($"{endpoint}?count={count}", null);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = $"{serviceName} ({endpoint}) başarıyla seed edildi." });
                }

                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Hata: {response.StatusCode} - {error}" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Sistem Hatası: {ex.Message}" });
            }
        }
    }
}
