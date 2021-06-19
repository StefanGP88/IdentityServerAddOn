using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components
{
    public interface IViewComponentReader
    {
        Task<IHtmlContent> ReadHtmlContent(string componentName, ViewContext viewContext);
        Task<string> ReadAsString(string componentName, ViewContext viewContext);
        Task<IHtmlContent> ReadHtmlContent(string componentName, object argument, ViewContext viewContext);
        Task<string> ReadAsString(string componentName, object argument, ViewContext viewContext);
    }
}
