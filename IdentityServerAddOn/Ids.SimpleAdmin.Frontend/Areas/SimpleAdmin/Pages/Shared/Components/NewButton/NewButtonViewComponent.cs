using Microsoft.AspNetCore.Mvc;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components.NewButton
{
    public class NewButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string partial, string element)
        {
            return View(new ButtonData
            {
                Partial = partial,
                Element = element
            });
        }
    }
    public class ButtonData
    {
        public string Partial { get; set; }
        public string Element { get; set; }
    }
}
