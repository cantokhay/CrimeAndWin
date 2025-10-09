using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Drawer
{
    public class _AdminDrawerPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
