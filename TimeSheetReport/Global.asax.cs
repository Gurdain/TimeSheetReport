using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TimeSheetReport
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Timers.Timer time = new System.Timers.Timer();
            time.Start();
            time.Interval = 60000; // 1 min //3.6e+6; // 1 hour
            time.Elapsed += time_elapsed;
        }

        public void time_elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            @TimeSheetReport.Controllers.HomeController.CheckTask();
            @TimeSheetReport.Controllers.HomeController.SendEmailFromQueue();
        }
    }
}
