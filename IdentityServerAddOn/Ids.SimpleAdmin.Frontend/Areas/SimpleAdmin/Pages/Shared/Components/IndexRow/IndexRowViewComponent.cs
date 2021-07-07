using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.IndexRow
{
    public class IndexRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string identifier)
        {
            return View(new IndexRowData
            {
                Title = title,
                Identifier = identifier
            });
        }
    }
    public class IndexRowData
    {
        public string Title { get; set; }
        public string Identifier { get; set; }
    }
}
