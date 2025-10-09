using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Header.Toolbar
{
    public class _AdminHeaderToolbarUserMenuPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
