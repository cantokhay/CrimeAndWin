using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Helpers
{
    public static class MenuDropdownHelper
    {
        public static string IsActive(this IHtmlHelper html,
                                      string controller = null,
                                      string action = null)
        {
            var routeData = html.ViewContext.RouteData;

            var currentController = routeData.Values["controller"]?.ToString();
            var currentAction = routeData.Values["action"]?.ToString();

            var controllerMatch = controller == null || controller == currentController;
            var actionMatch = action == null || action == currentAction;

            return controllerMatch && actionMatch ? "active" : "";
        }

        public static string IsMenuOpen(this IHtmlHelper html,
                                        string controller)
        {
            var routeData = html.ViewContext.RouteData;
            var currentController = routeData.Values["controller"]?.ToString();

            return currentController == controller ? "here show" : "";
        }

        public static string IsMenuOpen(this IHtmlHelper html,
                                        params string[] controllers)
        {
            var routeData = html.ViewContext.RouteData;
            var currentController = routeData.Values["controller"]?.ToString();

            return System.Linq.Enumerable.Contains(controllers, currentController) ? "here show" : "";
        }
    }
}
