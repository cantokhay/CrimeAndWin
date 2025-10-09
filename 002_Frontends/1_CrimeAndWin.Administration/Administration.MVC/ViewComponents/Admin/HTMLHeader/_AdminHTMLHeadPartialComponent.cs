using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.HTMLHeader
{
    public class _AdminHTMLHeadPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
