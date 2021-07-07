using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class IndexRowTagHelper : TagHelper
    {
        public string Title { get; set; } = nameof(CollapsableTagHelper);
        public string Identitfier { get; set; } = nameof(CollapsableTagHelper);


        private readonly IViewComponentReader _reader;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public IndexRowTagHelper(IViewComponentReader viewComponentReader)
        {
            _reader = viewComponentReader;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;

            var component = await _reader.ReadAsString("IndexRow", new { title = Title, identifier = Identitfier }, ViewContext).ConfigureAwait(false);
            var childContent = await output.GetChildContentAsync().ConfigureAwait(false);
            var content = childContent.GetContent();

            component = component.Replace("<!--content-->", content);

            output.Content.SetHtmlContent(component);


        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
