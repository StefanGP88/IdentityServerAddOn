using System.Collections.Generic;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class EditModel : BaseEditPage<ApiResourceContract, int>
    {
        public EditModel(IHandler<ApiResourceContract, int> handler) : base(handler) { }
        internal override void SetResourceProperties()
        {
            var claimsInfo = new ResourcePropertyInfo()
            {
                Name = "Claims",
                Columns = new List<string> { "Type" }
            };
            var propertiesInfo = new ResourcePropertyInfo()
            {
                Name = "Properties",
                Columns = new List<string> { "Value", "Key" }
            };
            var scopesInfo = new ResourcePropertyInfo()
            {
                Name = "Scopes",
                Columns = new List<string> { "Scope" }
            };
            var secretsInfo = new ResourcePropertyInfo()
            {
                Name = "Secrets",
                Columns = new List<string> { "Type", "Value", "Description", "Expiration" }
            };

            ResourceProperties.Add(claimsInfo);
            ResourceProperties.Add(propertiesInfo);
            ResourceProperties.Add(scopesInfo);
            ResourceProperties.Add(secretsInfo);
        }
    }
}
