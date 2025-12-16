using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lab0.Models;

public class LastVisitCookie
{
    private readonly RequestDelegate _next;
    public static readonly string CookieName = "VISIT";

    public LastVisitCookie(RequestDelegate @delegate)
    {
        _next = @delegate;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey(CookieName))
        {
            if (context.Request.Cookies.TryGetValue(CookieName, out string? value) && !string.IsNullOrWhiteSpace(value))
            {
                if (DateTime.TryParse(value, out var visitDate))
                {
                    context.Items[CookieName] = visitDate;
                }
                else
                {
                    context.Items[CookieName] = value;
                }
            }
        }
        else
        {
            context.Items[CookieName] = "First visit.";
        }

        context.Response.Cookies.Append(CookieName, DateTime.Now.ToString());
        await _next(context);
    }
}
