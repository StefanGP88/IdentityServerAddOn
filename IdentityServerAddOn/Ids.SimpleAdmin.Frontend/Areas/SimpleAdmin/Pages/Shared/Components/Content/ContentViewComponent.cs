using Ids.SimpleAdmin.Backend.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Content
{
    public class ContentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, ErrorSummary errorSummary)
        {
            return View(new ContentData
            {
                Title = title,
                ErrorCount= errorSummary.ErrorCount,
                HasError= errorSummary.HasError
            });
        }
    }
    public class ContentData
    {
        public string Title { get; set; }
        public int ErrorCount { get; set; }
        public bool HasError { get; set; }
    }
}
