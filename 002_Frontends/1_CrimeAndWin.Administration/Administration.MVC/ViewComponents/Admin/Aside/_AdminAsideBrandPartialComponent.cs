using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Aside
{
    public class _AdminAsideBrandPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
