using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Shared.Components
{
    public class ViewComponentReader : IViewComponentReader
    {
        private readonly IViewComponentDescriptorCollectionProvider _descriptorCollectionProvider;
        private readonly IViewComponentSelector _selector;
        private readonly IViewComponentInvokerFactory _factory;
        private readonly IViewBufferScope _bufferScope;

        public ViewComponentReader(
            IViewComponentDescriptorCollectionProvider descriptorCollectionProvider,
            IViewComponentSelector selector,
            IViewComponentInvokerFactory factory,
            IViewBufferScope bufferScope)
        {
            _descriptorCollectionProvider = descriptorCollectionProvider;
            _selector = selector;
            _factory = factory;
            _bufferScope = bufferScope;

        }

        public async Task<string> ReadAsString(string componentName, ViewContext viewContext)
        {
            var content = await ReadHtmlContent(componentName, viewContext).ConfigureAwait(false);
            return WriteToString(content);
        }

        public async Task<string> ReadAsString(string componentName, object argument, ViewContext viewContext)
        {
            var content = await ReadHtmlContent(componentName, argument, viewContext).ConfigureAwait(false);
            return WriteToString(content);
        }

        public async Task<IHtmlContent> ReadHtmlContent(string componentName, ViewContext viewContext)
        {
            var helper = GetComponentHelper(viewContext);
            return await helper.InvokeAsync(componentName).ConfigureAwait(false);
        }

        public async Task<IHtmlContent> ReadHtmlContent(string componentName, object argument, ViewContext viewContext)
        {
            var helper = GetComponentHelper(viewContext);
            return await helper.InvokeAsync(componentName, argument).ConfigureAwait(false);
        }
        private string WriteToString(IHtmlContent htmlContent)
        {
            var stringWriter = new StringWriter();
            htmlContent.WriteTo(stringWriter, HtmlEncoder.Default);
            return stringWriter.ToString();
        }
        private DefaultViewComponentHelper GetComponentHelper(ViewContext viewContext)
        {
            var helper = new DefaultViewComponentHelper(
                _descriptorCollectionProvider,
                HtmlEncoder.Default,
                _selector,
                _factory,
                _bufferScope);
            helper.Contextualize(viewContext);
            return helper;
        }
    }
}
