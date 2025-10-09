using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Aside.Menu
{
    public class _AdminAsideMenuMainDropdownPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
