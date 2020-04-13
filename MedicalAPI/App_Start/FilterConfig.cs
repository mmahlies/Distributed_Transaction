using MedicalAPI.Filter;
using System.Web;
using System.Web.Mvc;

namespace MedicalAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
       //     filters.Add(new TransactionFilter());
        }
    }
}
