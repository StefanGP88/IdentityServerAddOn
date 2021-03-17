using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared
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
        public virtual PartialViewResult OnGetPartial(string partialName)
        {
            partialName = partialName.FirstLetterToUpper();
            return Partial("TableRowPartials/_" + partialName, null);
        }
        public virtual PartialViewResult OnGetPartial<T>(string partialName, T model)
        {
            partialName = partialName.FirstLetterToUpper();
            return Partial("TableRowPartials/_" + partialName, model);
        }
    }
}
