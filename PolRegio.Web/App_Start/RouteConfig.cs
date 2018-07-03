using System.Web.Mvc;
using System.Web.Routing;

namespace PolRegio.Web.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "AccountActivaiton",
                "{lang}/konto/aktywacja/{token}",
                new
                {
                    controller = "PolRegioAccount",
                    action = "ActivateAccount",
                });

            routes.MapRoute(
                "AccountLogout",
                "{lang}/konto/wylogowanie",
                new
                {
                    controller = "PolRegioAccount",
                    action = "Logout",
                });

            routes.MapRoute(
                "AccountSocialMediaLogin",
                "{lang}/konto/logowanie-media-spolecznosciowe",
                new
                {
                    controller = "PolRegioAccount",
                    action = "SocialMediaLogin"
                });

            routes.MapRoute(
                "AccountDelete",
                "{lang}/konto/usun",
                new
                {
                    controller = "PolRegioAccount",
                    action = "DeleteAccount"
                });
        }
    }
}