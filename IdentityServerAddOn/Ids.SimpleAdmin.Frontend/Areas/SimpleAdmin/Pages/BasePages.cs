using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<IActionResult> OnGet(CancellationToken cancel = default)
        {
            List = await _handler.GetAll(PageNumber, PageSize, cancel).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPost(TIdentifier id, CancellationToken cancel = default)
        {
            List = await _handler.Delete(id, PageNumber, PageSize, cancel).ConfigureAwait(false);
            return Page();
        }
    }

    public abstract class BaseAddPage<TDataTransferObject> : BasePage<TDataTransferObject>
    {
        public List<ResourcePropertyInfo> ResourceProperties { get; set; } = new List<ResourcePropertyInfo>();

        public BaseAddPage(IHandler<TDataTransferObject> handler) : base(handler)
        {
            SetResourceProperties();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(TDataTransferObject dto, CancellationToken cancel = default)
        {
            var result = await _handler.Create(dto, PageNumber, PageSize, cancel).ConfigureAwait(false);
            return RedirectToPage("Index", new { PageNumber, PageSize });
        }

        internal PartialViewResult GetPartial<TPartial>(string partialName, TPartial dto)
        {
            return Partial("TableRowPartials/" + partialName, dto);
        }
        internal abstract void SetResourceProperties();

    }
}
