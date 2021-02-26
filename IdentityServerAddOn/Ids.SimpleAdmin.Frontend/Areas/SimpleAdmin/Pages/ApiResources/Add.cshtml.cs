using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class AddModel : BaseAddPage<ApiResourceContract>
    {
        public AddModel(IHandler<ApiResourceContract> handler) : base(handler) { }

        public PartialViewResult OnGetClaims(ApiResourceClaimsContract dto)
        {
            return Partial("_ClaimsTableRowPartial", dto);
        }
        public PartialViewResult OnGetProperties(ApiResourcePropertiesContract dto)
        {
            return Partial("_PropertiesTableRowPartial", dto);
        }
        public PartialViewResult OnGetScopes(ApiResourceScopesContract dto)
        {
            return Partial("_ScopesTableRowPartial", dto);
        }
        public PartialViewResult OnGetSecrets(ApiResourceSecretsContract dto)
        {
            return Partial("_SecretsTableRowPartial", dto);
        }

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
                Columns = new List<string> { "Property", "Key" }
            };
            var scopesInfo = new ResourcePropertyInfo()
            {
                Name = "Scopes",
                Columns = new List<string> { "Scope" }
            };
            var secretsInfo = new ResourcePropertyInfo()
            {
                Name = "Secrets",
                Columns = new List<string> { "Type", "Value" }
            };

            ResourceProperties.Add(claimsInfo);
            ResourceProperties.Add(propertiesInfo);
            ResourceProperties.Add(scopesInfo);
            ResourceProperties.Add(secretsInfo);

        }
    }
}
