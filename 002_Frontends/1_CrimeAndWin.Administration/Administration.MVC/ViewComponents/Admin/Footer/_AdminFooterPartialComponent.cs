using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Footer
{
    public class _AdminFooterPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
