using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Header.Navbar
{
    public class _AdminHeaderNavbarPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
