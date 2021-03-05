using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages
{
    public class BasePage<TData, TIdentifier> : PageModel
    {
        internal readonly IHandler<TData, TIdentifier> _handler;
        [FromQuery(Name = "pagesize")]
        public int PageSize { get; set; }
        [FromQuery(Name = "pagenumber")]
        public int PageNumber { get; set; }

        public BasePage(IHandler<TData, TIdentifier> handler)
        {
            _handler = handler;
        }
        public virtual PartialViewResult OnGetPartial(TIdentifier id, string partialName)
        {
            return Partial("TableRowPartials/_" + partialName, null);
        }
    }

    public class BaseIndexPage<TData, TIdentifier> : BasePage<TData, TIdentifier>
    {
        public ListDto<TData> List { get; set; }
        public BaseIndexPage(IHandler<TData, TIdentifier> handler) : base(handler) { }

        public virtual async Task<IActionResult> OnGet(CancellationToken cancel = default)
        {
            List = await _handler.GetAll(PageNumber, PageSize, cancel).ConfigureAwait(false);
            return Page();
        }

        public virtual async Task<IActionResult> OnPost(TIdentifier id, CancellationToken cancel = default)
        {
            List = await _handler.Delete(id, PageNumber, PageSize, cancel).ConfigureAwait(false);
            return Page();
        }
    }

    public class BaseInfoPage<TData, TIdentifier> : BasePage<TData, TIdentifier> where TData : Identifyable<TIdentifier>
    {
        public TData Data { get; set; }
        public BaseInfoPage(IHandler<TData, TIdentifier> handler) : base(handler)
        {
        }

        public virtual async Task<IActionResult> OnGet(TIdentifier id , CancellationToken cancel = default)
        {
            Data = await _handler.Get(id, PageNumber, PageSize, cancel).ConfigureAwait(false);
            return Page();
        }

        public virtual async Task<IActionResult> OnPost(TData dto, CancellationToken cancel = default)
        {
            _ = await _handler.Update(dto, PageNumber, PageSize, cancel).ConfigureAwait(false);
            return RedirectToPage("Index", new { PageNumber, PageSize });
        }
    }
}
