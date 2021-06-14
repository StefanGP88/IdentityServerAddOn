using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public HeaderViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var area = _contextAccessor.HttpContext.Request.RouteValues["area"]?.ToString();
            var page = _contextAccessor.HttpContext.Request.RouteValues["page"]?.ToString();

            var model = GetHeaderData(area, page);

            return View(model);
        }

        HeaderData GetHeaderData(string area, string page) => (area, page) switch
        {
            //index
            ("SimpleAdmin", "/Index") => new HeaderData { Title = "Welcome to Simple Admin", Subtitle = "Select area to manage" },
            //area overviews
            ("SimpleAdmin", "/Clients/Index") => new HeaderData { Title = "Clients", Subtitle = "Clients overview" },
            ("SimpleAdmin", "/User/Index") => new HeaderData { Title = "Users", Subtitle = "Users overview" },
            ("SimpleAdmin", "/Roles/Index") => new HeaderData { Title = "Roles", Subtitle = "Roles overview" },
            ("SimpleAdmin", "/IdentityResources/Index") => new HeaderData { Title = "Identity resources", Subtitle = "Identity resources overview" },
            ("SimpleAdmin", "/ApiScopes/Index") => new HeaderData { Title = "Api scopes", Subtitle = "Api scopes overview" },
            ("SimpleAdmin", "/ApiResources/Index") => new HeaderData { Title = "Api resources", Subtitle = "Api resources overview" },
            //info pages
            ("SimpleAdmin", "/Clients/Info") => new HeaderData { Title = "Client", Subtitle = "Client information" },
            ("SimpleAdmin", "/User/Info") => new HeaderData { Title = "User", Subtitle = "User information" },
            ("SimpleAdmin", "/Roles/Info") => new HeaderData { Title = "Role", Subtitle = "Role information" },
            ("SimpleAdmin", "/IdentityResources/Info") => new HeaderData { Title = "Identity resource", Subtitle = "Identity resource information" },
            ("SimpleAdmin", "/ApiScopes/Info") => new HeaderData { Title = "Api scope", Subtitle = "Api scope information" },
            ("SimpleAdmin", "/ApiResources/Info") => new HeaderData { Title = "Api resource", Subtitle = "Api resource information" },
            //default
            _ => new HeaderData { Title = "Welcome to Simple Admin", Subtitle = "" }
        };
    }


    public class HeaderData
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}
