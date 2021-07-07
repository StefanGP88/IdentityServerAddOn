using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Table
{
    public class TableViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string bodyId)
        {
            return View(new TableData { BodyId = bodyId });
        }
    }
    public class TableData
    {
        public string BodyId { get; set; }
    }
}
