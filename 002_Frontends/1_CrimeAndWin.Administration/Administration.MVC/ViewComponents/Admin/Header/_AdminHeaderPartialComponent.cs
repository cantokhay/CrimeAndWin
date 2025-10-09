using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Header
{
    public class _AdminHeaderPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
