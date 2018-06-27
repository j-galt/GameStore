using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GameStore.Web.App_Start;
using GameStore.Web.Infrastructure;

namespace GameStore.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var logger = NLog.LogManager.GetLogger("GlobalApplicationLog");
            logger.Info("Configuring application started.");

            try
            {
                AutofacConfig.RegisterComponents();
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);

                //GlobalConfiguration.Configuration.Filters.Add(new CustomErrorFilter(logger));
                //GlobalConfiguration.Configuration.Filters.Add(new CustomValidationFilter(logger));
            }
            catch (Exception exception)
            {
                logger.Fatal(exception, "Configuring application failed.");
                throw;
            }

            logger.Info("Configuring application completed. Starting application.");           
        }
    }
}