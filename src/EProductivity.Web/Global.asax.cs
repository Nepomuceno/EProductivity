using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Telemetry.Services;

namespace EProductivity.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ServerAnalytics.Start("beab1727-dedc-490b-91c2-1d9a5ffbb78d");
        }
        protected void Application_BeginRequest(object sender,EventArgs e)
        {
            ServerAnalytics.BeginRequest();
            ServerAnalytics.CurrentRequest.LogEvent(Request.Url.AbsolutePath);
        }
    }
}
