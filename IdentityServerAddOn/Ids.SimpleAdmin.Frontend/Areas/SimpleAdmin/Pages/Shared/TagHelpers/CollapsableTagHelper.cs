using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class CollapsableTagHelper : TagHelper
    {
        public string Title { get; set; } = nameof(CollapsableTagHelper);
        public ErrorSummary ErrorSummary { get; set; } = new();


        private readonly IViewComponentReader _reader;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public CollapsableTagHelper(IViewComponentReader viewComponentReader)
        {
            _reader = viewComponentReader;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;

            var component = await _reader.ReadAsString("Content", new { Title, ErrorSummary }, ViewContext).ConfigureAwait(false);
            var childContent = await output.GetChildContentAsync().ConfigureAwait(false);
            var content = childContent.GetContent();

            component = component.Replace("<!--Body-->", content);
            output.TagName = Title.Replace(" ", "").ToLowerInvariant();

            output.Content.SetHtmlContent(component);


        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
