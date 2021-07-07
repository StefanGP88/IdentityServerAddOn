using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class ListTableTagHelper : TagHelper
    {

        private readonly IViewComponentReader _reader;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public string BodyId { get; set; } = "";
        public ListTableTagHelper(IViewComponentReader viewComponentReader)
        {
            _reader = viewComponentReader;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;
            
            var childContent = await output.GetChildContentAsync().ConfigureAwait(false);
            var content = childContent.GetContent();
            var component = await _reader.ReadAsString("Table", BodyId, ViewContext).ConfigureAwait(false);

            component = component.Replace("<!--Content-->", content);

            output.Content.SetHtmlContent(component);
        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
