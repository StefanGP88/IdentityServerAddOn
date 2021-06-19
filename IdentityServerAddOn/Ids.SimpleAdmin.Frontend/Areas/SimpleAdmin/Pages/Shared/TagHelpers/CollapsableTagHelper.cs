﻿using Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.TagHelpers
{
    public class CollapsableTagHelper : TagHelper
    {
        public string Title { get; set; } = nameof(CollapsableTagHelper);


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


            var asstring = await _reader.ReadAsString("Content", Title, ViewContext).ConfigureAwait(false);

            var cc = await output.GetChildContentAsync().ConfigureAwait(false);
           var teetetetteteet= cc.GetContent();

            asstring = asstring.Replace("<!--Body-->", teetetetteteet);

            var a = output.IsContentModified;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = Title.Replace(" ", "").ToLowerInvariant();

            output.Content.SetHtmlContent(asstring);
           

        }
        public override void Init(TagHelperContext context)
        {
            context.Reinitialize(context.Items, context.UniqueId);
        }
    }
}
