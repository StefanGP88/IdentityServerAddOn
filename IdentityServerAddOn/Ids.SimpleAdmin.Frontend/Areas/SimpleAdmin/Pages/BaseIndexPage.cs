using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages
{
    public class BaseIndexPage<TResource> : PageModel
    {
        internal readonly IHandler<TResource> _handler;
        [FromQuery(Name = "pagesize")]
        public int PageSize { get; set; }
        [FromQuery(Name = "pagenumber")]
        public int PageNumber { get; set; }
        public ListDto<TResource> List { get; set; }

        public BaseIndexPage(IHandler<TResource> handler)
        {
            _handler = handler;
        }

        public async Task<IActionResult> OnGet()
        {
            List = await _handler.GetAll(PageNumber, PageSize, default).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            await _handler.Delete(id).ConfigureAwait(false);
            return Page();
        }
    }
}
