using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages
{
    public class BasePageModel<TResource, TProperties> : PageModel 
    {
        [FromQuery(Name = "pagesize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "pagenumber")]
        public int PageNumber { get; set; }

        public ListDto<TResource> List { get; set; }
        public static TProperties Properties { get; } = (TProperties)Activator.CreateInstance(typeof(TProperties));
    }
}
