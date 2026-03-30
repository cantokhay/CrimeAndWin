using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Drawer.TopDrawer
{
    public class _AdminDrawerTopDrawerPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
