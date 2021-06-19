using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Content
{
    public class ContentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title)
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
