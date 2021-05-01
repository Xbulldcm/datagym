using DataGYM.Filtro;
using System.Web;
using System.Web.Mvc;

namespace DataGYM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ValidaSesion());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
