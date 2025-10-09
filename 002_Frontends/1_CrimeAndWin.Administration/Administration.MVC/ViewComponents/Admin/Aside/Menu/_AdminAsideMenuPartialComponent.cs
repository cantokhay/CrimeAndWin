using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Aside.Menu
{
    public class _AdminAsideMenuPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
