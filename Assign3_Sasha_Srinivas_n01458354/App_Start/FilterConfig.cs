using System.Web;
using System.Web.Mvc;

namespace Assign3_Sasha_Srinivas_n01458354
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
