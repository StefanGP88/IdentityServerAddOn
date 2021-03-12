using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared
{
    public class BaseInfoPage<TData, TIdentifier> : BasePage<TData, TIdentifier> where TData : Identifyable<TIdentifier>
    {
        public TData Data { get; set; }
        public BaseInfoPage(IHandler<TData, TIdentifier> handler) : base(handler) { }

        public virtual async Task<IActionResult> OnGet(TIdentifier id, CancellationToken cancel = default)
        {
            Data = await _handler.Get(id, cancel).ConfigureAwait(false);
            return Page();
        }

        public virtual async Task<IActionResult> OnPost(TData dto, CancellationToken cancel = default)
        {
            if (!ModelState.IsValid)
            {
                Data = dto;
                return Page();
            }
            if (dto.Id == null)
                _ = await _handler.Create(dto, cancel).ConfigureAwait(false);
            else
                _ = await _handler.Update(dto, cancel).ConfigureAwait(false);

            return RedirectToPage("Index", new { PageNumber, PageSize });
        }
    }
}
