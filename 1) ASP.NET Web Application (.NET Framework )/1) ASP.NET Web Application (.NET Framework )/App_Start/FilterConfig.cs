using System.Web;
using System.Web.Mvc;

namespace _1__ASP.NET_Web_Application__.NET_Framework__
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
