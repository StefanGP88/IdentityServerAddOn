using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.FormSection
{
    public class FormSectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string data)
        {
            return View(data);
        }
    }
}
