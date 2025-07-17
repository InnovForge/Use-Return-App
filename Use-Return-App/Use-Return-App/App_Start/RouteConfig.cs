using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Use_Return_App
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Off;
            //routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute(
            routeName: "thanhtoan",
            routeUrl: "checkout",
            physicalFile: "~/Checkout.aspx");

            routes.MapPageRoute(
         routeName: "paymentcallback",
         routeUrl: "checkout/paymentcallback/{slug}",
         physicalFile: "~/PaymentCallback.aspx");

            routes.MapPageRoute(
            routeName: "ChiTietDoDung",
            routeUrl: "{slug}",
            physicalFile: "~/ItemDetail.aspx"  
        );
            routes.Ignore("{resource}.axd/{*pathInfo}");
        }
    }
}
