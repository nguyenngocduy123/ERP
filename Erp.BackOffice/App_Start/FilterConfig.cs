using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SecurityFilter());
        }
    }
}