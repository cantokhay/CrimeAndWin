using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.ViewComponents.Admin.Scripts
{
    public class _AdminScriptsPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
