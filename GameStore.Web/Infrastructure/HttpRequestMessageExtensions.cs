using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace GameStore.Web.Infrastructure
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return IPAddress.Parse(((HttpContextBase)request
                    .Properties["MS_HttpContext"])
                    .Request.UserHostAddress)
                    .ToString();
            }

            return null;
        }
    }
}