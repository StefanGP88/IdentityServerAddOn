using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class NewTagHelper : TagHelper
    {
        public string PartialName { get; set; } = nameof(DeleteTagHelper);
        public string AppendElement { get; set; } = nameof(DeleteTagHelper);

        private readonly IViewComponentReader _reader;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public NewTagHelper(IViewComponentReader viewComponentReader)
        {
            _reader = viewComponentReader;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;

            var component = await _reader.ReadAsString("NewButton", new { partial = PartialName, element = AppendElement }, ViewContext).ConfigureAwait(false);
            output.TagName = null;

            output.Content.SetHtmlContent(component);


        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
