using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.Table
{
    public class TableViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
