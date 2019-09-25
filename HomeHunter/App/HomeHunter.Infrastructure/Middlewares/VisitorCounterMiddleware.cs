using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace HomeHunter.Infrastructure.Middlewares
{
    public class VisitorCounterMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public VisitorCounterMiddleware(RequestDelegate requestDelegate
            )
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            string visitorId = context.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                var newVisitorId = Guid.NewGuid().ToString();

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
