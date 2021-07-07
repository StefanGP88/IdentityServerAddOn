using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Content
{
    public class ContentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title)
        {
            return View(new ContentData
            {
                Title = title
            });
        }
    }
    public class ContentData
    {
        public string Title { get; set; }
    }
}
