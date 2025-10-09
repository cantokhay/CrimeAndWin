using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Header.Toolbar
{
    public class _AdminHeaderToolbarPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
