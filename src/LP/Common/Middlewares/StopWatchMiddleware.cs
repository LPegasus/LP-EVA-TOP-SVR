using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using LP.Helpers;

namespace LP.Common.Middlewares {
    public class StopWatchMiddleware : AbstractMiddleware {

        public StopWatchMiddleware(RequestDelegate next) : base(next) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            context.Items["sw"] = sw;
            try
            {
                var cancelSource = new CancellationTokenSource(1000);
                context.RequestAborted = cancelSource.Token;
                var t1 = Task.Factory.StartNew(() =>
                {
                    return Next.Invoke(context);
                });

                t1.Wait(cancelSource.Token);
                await t1;
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("timeout!");
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}
