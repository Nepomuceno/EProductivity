using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EProductivity.Core.Model.Data;
using EProductivity.Core.Model.Data.EF;
using EProductivity.Core.Service;
using Microsoft.ApplicationInsights.Telemetry.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

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
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
#if !DEBUG
            ServerAnalytics.BeginRequest();
            ServerAnalytics.CurrentRequest.LogEvent(Request.Url.AbsolutePath);
#endif
        }
    }
}
