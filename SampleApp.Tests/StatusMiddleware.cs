using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests
{
    public class StatusMiddleware
    {
        private readonly RequestDelegate _next;
        public StatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/ping"))
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("pong");
                return;
            }
            await _next(context);
        }

    }

}
