using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ApiScopeMapper : AbstractMapper<ApiScopeContract, ApiScope>
    {

        private readonly IMapper<ApiScopePropertiesContract, ApiScopeProperty> _property;
        private readonly IMapper<ApiScopeClaimsContract, ApiScopeClaim> _claim;
        public ApiScopeMapper(IMapper<ApiScopePropertiesContract, ApiScopeProperty> propertyMapper,
            IMapper<ApiScopeClaimsContract, ApiScopeClaim> claimMapper)
        {
            _property = propertyMapper;
            _claim = claimMapper;
        }
        public override ApiScopeContract ToContract(ApiScope model)
        {
            this.ThrowIfNull(model);
            return new ApiScopeContract
            {
                Description = model.Description,
                DisplayName = model.DisplayName,
                Emphasize = model.Emphasize,
                Enabled = model.Enabled,
                Name = model.Name,
                Id = model.Id,
                Required = model.Required,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                Properties = model.Properties?.ConvertAll(_property.ToContract),
                UserClaims = model.UserClaims?.ConvertAll(_claim.ToContract)
            };
        }

        public override ApiScope ToModel(ApiScopeContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ApiScope UpdateModel(ApiScope model, ApiScopeContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Description = contract.Description;
            model.DisplayName = contract.DisplayName;
            model.Emphasize = contract.Emphasize;
            model.Enabled = contract.Enabled;
            model.Name = contract.Name;
            model.Required = contract.Required;
            model.ShowInDiscoveryDocument = contract.ShowInDiscoveryDocument;
            model.Properties = _property.UpdateList(model.Properties, contract.Properties);
            model.UserClaims = _claim.UpdateList(model.UserClaims, contract.UserClaims);
            return model;
        }
    }
    public class ApiScopeClaimMapper : AbstractMapper<ApiScopeClaimsContract, ApiScopeClaim>
    {
        public override ApiScopeClaimsContract ToContract(ApiScopeClaim model)
        {
            this.ThrowIfNull(model);
            return new ApiScopeClaimsContract
            {
                ApiScopeId = model.ScopeId,
                Id = model.Id,
                Type = model.Type
            };
        }

        public override ApiScopeClaim ToModel(ApiScopeClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ApiScopeClaim UpdateModel(ApiScopeClaim model, ApiScopeClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Type = contract.Type;
            return model;
        }
    }
    public class ApiScopePropertyMapper : AbstractMapper<ApiScopePropertiesContract, ApiScopeProperty>
    {
        public override ApiScopePropertiesContract ToContract(ApiScopeProperty model)
        {
            this.ThrowIfNull(model);
            return new ApiScopePropertiesContract
            {
                Key = model.Key,
                Value = model.Value
            };
        }

        public override ApiScopeProperty ToModel(ApiScopePropertiesContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ApiScopeProperty UpdateModel(ApiScopeProperty model, ApiScopePropertiesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Key = contract.Key;
            model.Value = contract.Value;
            return model;
        }
    }
}
