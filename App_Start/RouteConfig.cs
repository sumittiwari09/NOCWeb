using System.Web.Mvc;
using System.Web.Routing;

namespace NewZapures_V2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //            routes.MapRoute(
            //name: "MainPage",
            //url: "Register",
            //defaults: new { controller = "LoginSignup", action = "Index" }
            //);
            routes.MapRoute(
name: "RegisteredUser",
url: "Registered_User",
defaults: new { controller = "Home", action = "RegistratedUser" }
);
            routes.MapRoute(
name: "MyProfileView",
url: "Registered_User/Profile/{Id}",
defaults: new { controller = "Home", action = "ProfileView", Id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "MyPaymentView",
url: "Registered_User/PaymentInformation/{Id}",
defaults: new { controller = "Home", action = "PaymentDetail", Id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "RateMaster",
url: "Rate_Information",
defaults: new { controller = "Admin", action = "RateMaster" }
);
            routes.MapRoute(
           name: "Gst_Master",
           url: "Gst-Applicabler",
           defaults: new { controller = "Admin", action = "GstApplicable" }
           );
            routes.MapRoute(
        name: "Detail",
        url: "Detail",
        defaults: new { controller = "Admin", action = "Detail" }
        );

      //      routes.MapRoute(
      //name: "NodirectAccess",
      //url: "NodirectAccess",
      //defaults: new { controller = "Welcome", action = "NoDirectAccess" }
      //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "login-alt", id = UrlParameter.Optional }









            );
        }
    }
}
