using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Infrastructure.Middlewares
{
    public class VisitorCounterMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly IHttpContextAccessor accessor;

        public VisitorCounterMiddleware(RequestDelegate requestDelegate, IHttpContextAccessor accessor)
        {
            this.requestDelegate = requestDelegate;
            this.accessor = accessor;
        }

        public async Task Invoke(HttpContext context)
        {
            string visitorId = context.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                var newVisitorId = Guid.NewGuid().ToString();
                var ip = this.accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                context.Response.Cookies.Append("VisitorId", newVisitorId, new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                });
            }
            await this.requestDelegate(context);
        }
    }
}
