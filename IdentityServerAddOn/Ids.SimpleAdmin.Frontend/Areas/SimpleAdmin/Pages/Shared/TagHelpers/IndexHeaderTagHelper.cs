using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class IndexHeaderTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;

            var childContent = await output.GetChildContentAsync().ConfigureAwait(false);

            var startTag = "<div class=\"row text-left\" style=\"padding: .375rem .75rem; \">";
            var content = childContent.GetContent();
            var endTag = "</div>";

            output.Content.SetHtmlContent(startTag + content + endTag);
        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
