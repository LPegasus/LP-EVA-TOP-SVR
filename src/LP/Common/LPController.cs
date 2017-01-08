using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LP.Common
{
    public class LPController : Controller
    {
        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.RequestServices == null) return;
            base.OnActionExecuting(context);
        }

        [NonAction]
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.RequestServices == null) return;
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
