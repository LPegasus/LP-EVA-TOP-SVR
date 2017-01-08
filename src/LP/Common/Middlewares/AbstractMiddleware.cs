using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LP.Common.Middlewares
{
    public abstract class AbstractMiddleware
    {
        protected virtual RequestDelegate Next { get; set; }
        protected AbstractMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        public abstract Task InvokeAsync(HttpContext context);
    }
}
