using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages
{
    public class BasePageModel<T> : PageModel
    {
        [FromQuery(Name = "pagesize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "pagenumber")]
        public int PageNumber { get; set; }

        public ListDto<T> List { get; set; }
    }
}
