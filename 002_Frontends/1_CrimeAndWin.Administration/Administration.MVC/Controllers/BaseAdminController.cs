using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Administration.MVC.Controllers
{
    /// <summary>
    /// Tüm Admin Controller'lar için önleyici kapsayıcı (Preventive Container) temel sınıf.
    /// </summary>
    public abstract class BaseAdminController : Controller
    {
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// API yanıtlarını kontrol eder ve hataları ModelState'e aktarır.
        /// Ayrıca SweetAlert için TempData bildirimlerini hazırlar.
        /// </summary>
        protected async Task<bool> HandleApiResultAsync(HttpResponseMessage response, string successMessage = "İşlem başarıyla tamamlandı.")
        {
            if (response.IsSuccessStatusCode)
            {
                NotifySuccess(successMessage);
                return true;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetailsDTO>(content, options);

                    if (problemDetails?.Errors != null)
                    {
                        foreach (var error in problemDetails.Errors)
                        {
                            foreach (var message in error.Value)
                                ModelState.AddModelError(error.Key, message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz istek gönderildi.");
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "API hata detayları çözümlenemedi.");
                }
                
                NotifyError("Lütfen formdaki hataları kontrol edin.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                NotifyError("Bu işlem için yetkiniz bulunmamaktadır.");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                NotifyWarning("İlgili kayıt bulunamadı.");
            }
            else
            {
                NotifyError($"Sunucu Hatası ({(int)response.StatusCode}): Lütfen daha sonra tekrar deneyin.");
            }

            return false;
        }

        #region Notification Helpers (SweetAlert Entegrasyonu İçin)

        protected void NotifySuccess(string message) => SetTempData("success", "Başarılı!", message);
        protected void NotifyError(string message) => SetTempData("error", "Hata!", message);
        protected void NotifyWarning(string message) => SetTempData("warning", "Uyarı!", message);
        protected void NotifyInfo(string message) => SetTempData("info", "Bilgi", message);

        protected void SetTempData(string type, string title, string message)
        {
            TempData["Alert_Type"] = type;     // success, error, warning, info
            TempData["Alert_Title"] = title;
            TempData["Alert_Message"] = message;
        }

        #endregion

        // ProblemDetails deserileştirme için yardımcı sınıf
        public class ValidationProblemDetailsDTO
        {
            public string Title { get; set; }
            public int Status { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }
    }
}
