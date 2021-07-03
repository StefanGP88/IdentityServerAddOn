﻿using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class DeleteTagHelper : TagHelper
    {
        public string Title { get; set; } = nameof(DeleteTagHelper);
        public string Target { get; set; } = nameof(DeleteTagHelper);

        private readonly IViewComponentReader _reader;
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public DeleteTagHelper(IViewComponentReader viewComponentReader)
        {
            _reader = viewComponentReader;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.Clear();
            output.TagMode = TagMode.StartTagAndEndTag;

            var component = await _reader.ReadAsString("DeleteButton", new { title = Title, target = Target }, ViewContext).ConfigureAwait(false);
            output.TagName = "td";

            output.Content.SetHtmlContent(component);


        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
