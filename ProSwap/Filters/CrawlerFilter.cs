using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSwap.Filters
{
    public class CrawlerFilter : ActionFilterAttribute
    {
        // This will prevent the page from loading if the web user is a know web crawler (used GoogleDevTools Network conditions to simulate this through use of GoogleBot)

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Browser.Crawler)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}