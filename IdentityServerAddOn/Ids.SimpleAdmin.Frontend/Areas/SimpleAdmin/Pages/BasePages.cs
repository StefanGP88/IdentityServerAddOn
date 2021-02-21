using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages
{
    public class BasePage<TDataTransferObject> : PageModel
    {
        internal readonly IHandler<TDataTransferObject> _handler;
        [FromQuery(Name = "pagesize")]
        public int PageSize { get; set; }
        [FromQuery(Name = "pagenumber")]
        public int PageNumber { get; set; }

        public BasePage(IHandler<TDataTransferObject> handler)
        {
            _handler = handler;
        }
    }

    public class BaseIndexPage<TDataTransferObject, TIdentifier> : BasePage<TDataTransferObject>
    {
        public ListDto<TDataTransferObject> List { get; set; }
        public BaseIndexPage(IHandler<TDataTransferObject> handler) : base(handler) { }

        public async Task<IActionResult> OnGet()
        {
            List = await _handler.GetAll(PageNumber, PageSize, default).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPost(TIdentifier id)
        {
            List = await _handler.Delete(id, PageNumber, PageSize, default).ConfigureAwait(false);
            return Page();
        }
    }

    public class BaseAddPage<TDataTransferObject> : BasePage<TDataTransferObject>
    {
        public BaseAddPage(IHandler<TDataTransferObject> handler) : base(handler) { }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}
