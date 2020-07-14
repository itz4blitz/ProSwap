using ProSwap.Filters;
using System.Web;
using System.Web.Mvc;

namespace ProSwap
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogRequestFilter());
        }
    }
}
