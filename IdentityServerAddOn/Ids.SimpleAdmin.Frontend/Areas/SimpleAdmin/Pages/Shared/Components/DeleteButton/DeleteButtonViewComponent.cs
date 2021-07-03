using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.DeleteButton
{
    public class DeleteButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string target)
        {
            return View(new ButtonDataData
            {
                Title = title,
                Target = target
            });
        }
    }
    public class ButtonDataData
    {
        public string Title { get; set; }
        public string Target { get; set; }
    }
}
