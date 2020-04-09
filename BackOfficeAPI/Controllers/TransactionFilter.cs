using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Controllers
{
    internal class TransactionFilter : IActionFilter
    {
        private DbContext _BackOfficeDBContext;
        public TransactionFilter(DbContext BackOfficeDBContext)
        {
            _BackOfficeDBContext = BackOfficeDBContext;
            var x = "";
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}