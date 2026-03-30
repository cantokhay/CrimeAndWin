using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Drawer.BottomDrawer
{
    public class _AdminDrawerBottomDrawerPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
