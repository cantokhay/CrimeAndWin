using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Administration.MVC.Filters
{
    public class AdminExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public AdminExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            // Beklenmedik hataları (Network failure, NullReference, vb.) yakalayarak
            // kullanıcının sistem dışına atılmasını engeller.
            
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // TempData üzerinden hatayı bildirebilmek için ITempDataDictionary kullanıyoruz
            var tempData = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>()
                .GetTempData(context.HttpContext);
            
            tempData["Alert_Type"] = "error";
            tempData["Alert_Title"] = "Beklenmedik Hata!";
            tempData["Alert_Message"] = $"Sistemde bir hata oluştu: {context.Exception.Message}. Lütfen yöneticinizle iletişime geçin.";

            // Kullanıcıyı mevcut sayfada tutmak veya hata sayfasına yönlendirmek
            // Amaç: Patlayıp beyaz ekran göstermemek
            context.Result = new RedirectToActionResult(action, controller, context.RouteData.Values);
            
            context.ExceptionHandled = true;
        }
    }
}
