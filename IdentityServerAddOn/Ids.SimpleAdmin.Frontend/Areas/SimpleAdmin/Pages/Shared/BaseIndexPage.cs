using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared
{
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
}
