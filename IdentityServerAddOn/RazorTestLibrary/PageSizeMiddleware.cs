using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorTestLibrary
{
    public class PageSizeMiddleware : IMiddleware
    {
        private readonly string defaultPageSize = "10";
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var area = context.Request.RouteValues["area"]?.ToString();
            var page = context.Request.RouteValues["page"]?.ToString();
            var queryParams = QueryHelpers.ParseNullableQuery(context.Request.QueryString.Value);

            if (Equals(area, "simpleadmin") && !Equals(page, "/index"))
            {
                if (queryParams == null)
                {
                    queryParams = new Dictionary<string, StringValues>();
                }
                if (!queryParams.ContainsKey("pagesize"))
                {
                    queryParams.Add("pagesize", GetPageSizeCookie(context));
                }
                else
                {
                    SetPageSizeCookie(context, queryParams["pagesize"]);
                }
                context.Request.QueryString = QueryString.Create(queryParams);
            }
            await next.Invoke(context).ConfigureAwait(false);
        }

        private string GetPageSizeCookie(HttpContext context)
        {
            var pageSizeCookie = context.Request.Path.ToString();

            if (context.Request.Cookies.ContainsKey(pageSizeCookie))
            {
                return context.Request.Cookies[pageSizeCookie];
            }
            else
            {
                SetPageSizeCookie(context, defaultPageSize);
                return defaultPageSize;
            }
        }
        private void SetPageSizeCookie(HttpContext context, string size)
        {
            var pageSizeCookie = context.Request.Path.ToString();
            context.Response.Cookies.Append(pageSizeCookie, size);
        }

#nullable enable
        private bool Equals(string? a, string? b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
#nullable disable
    }
}
