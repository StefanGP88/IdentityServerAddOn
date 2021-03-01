using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.ApiResources
{
    public class AddModel : BaseAddPage<ApiResourceContract, int>
    {
        public AddModel(IHandler<ApiResourceContract, int> handler) : base(handler) { }

        public PartialViewResult OnGetClaims(ApiResourceClaimsContract dto)
        {
            return GetPartial("_Claims", dto);
        }
        public PartialViewResult OnGetProperties(ApiResourcePropertiesContract dto)
        {
            return GetPartial("_Properties", dto);
        }
        public PartialViewResult OnGetScopes(ApiResourceScopesContract dto)
        {
            return GetPartial("_Scopes", dto);
        }
        public PartialViewResult OnGetSecrets(ApiResourceSecretsContract dto)
        {
            return GetPartial("_Secrets", dto);
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
