﻿using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared
{
    [Authorize]
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
            return Partial("Partials/_" + partialName, null);
        }     
    }
}
