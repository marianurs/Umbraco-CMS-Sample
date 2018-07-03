using PolRegio.Web.Filters;
using System.Web.Mvc;

namespace PolRegio.Web.App_Start
{
    public static class FiltersConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PolRegioAuthorizationFilter());
        }
    }
}