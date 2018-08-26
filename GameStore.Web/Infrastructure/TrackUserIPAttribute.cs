using Autofac.Extras.NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GameStore.Web.Infrastructure
{
    public class TrackUserIPAttribute : ActionFilterAttribute
    {
        public ILogger Logger { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var ip = actionExecutedContext.Request.GetClientIpAddress();
            //Logger.Info("User IP: " + ip);
        }
    }
}