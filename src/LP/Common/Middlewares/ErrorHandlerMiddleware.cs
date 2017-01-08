using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LP.Common.Middlewares
{
    public class ErrorHandlerMiddleware : AbstractMiddleware
    {
        public ErrorHandlerMiddleware(RequestDelegate next) : base(next) { }
        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                if (e is OperationCanceledException)
                {
                    context.Response.StatusCode = 200;
                    Debug.WriteLine("Canceled");
                }
                else if (e is AggregateException)
                {
                    var ex = e as AggregateException;
                    var sb = new System.Text.StringBuilder();
                    foreach (var _e in ex.Flatten().InnerExceptions)
                    {
                        sb.AppendLine(_e.Message);
                    }
                    await context.Response.WriteAsync(sb.ToString());
                }
                else
                {
                    Debug.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}
