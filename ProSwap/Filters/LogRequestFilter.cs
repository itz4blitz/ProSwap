using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSwap.Filters
{
    public class LogRequestFilter : ActionFilterAttribute
    {
        // This provides a log within the output file that tracks the information of the users of the service

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var log = new
            {
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Action = filterContext.ActionDescriptor.ActionName,
                IP = filterContext.HttpContext.Request.UserHostAddress,
                DateTime = filterContext.HttpContext.Timestamp
            };

            Debug.WriteLine(JsonConvert.SerializeObject(log));
        }
    }
}