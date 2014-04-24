using System.Web.Mvc;
using System.Web.Routing;

namespace HipChat.Widget.Mvc
{
    public class Bootstrapper
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized)
                return;

            var namespaces = new[] { "HipChat.Widget.Mvc" };
            var routes = RouteTable.Routes;

            var hipChatWidgetRoute = Settings.Route;
            
            routes.MapRoute(
                "HipChat.Widget.Mvc",
                string.Format("{0}/{{resource}}", hipChatWidgetRoute),
                new
                {
                    controller = "HipChatWidget",
                    action = "Index",
                    resource = UrlParameter.Optional
                },
                null,
                namespaces);

            if (hipChatWidgetRoute != "HipChatWidget" && Settings.IgnoreDefaultRoute)
            {
                routes.IgnoreRoute("HipChatWidget");
                routes.IgnoreRoute("HipChatWidget/{*pathinfo}");
            }

            _initialized = true;
        }
    }
}