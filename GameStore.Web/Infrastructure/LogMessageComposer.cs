using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Infrastructure
{
    public static class LogMessageComposer
    {
        public static string Compose<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}