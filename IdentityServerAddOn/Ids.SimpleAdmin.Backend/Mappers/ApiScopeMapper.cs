using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ApiScopeMapper : AbstractMapper<ApiScopeContract, ApiScope>
    {

        private readonly IMapper<PropertyContract, ApiScopeProperty> _property;
        private readonly IMapper<ClaimsContract, ApiScopeClaim> _claim;
        public ApiScopeMapper(IMapper<PropertyContract, ApiScopeProperty> propertyMapper,
            IMapper<ClaimsContract, ApiScopeClaim> claimMapper)
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
                Claims = model.UserClaims?.ConvertAll(_claim.ToContract)
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
            model.UserClaims = _claim.UpdateList(model.UserClaims, contract.Claims);
            return model;
        }
    }
    public class ApiScopeClaimMapper : AbstractMapper<ClaimsContract, ApiScopeClaim>
    {
        public override ClaimsContract ToContract(ApiScopeClaim model)
        {
            this.ThrowIfNull(model);
            return new ClaimsContract
            {
                Id = model.Id,
                Type = model.Type
            };
        }

        public override ApiScopeClaim ToModel(ClaimsContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ApiScopeClaim UpdateModel(ApiScopeClaim model, ClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Type = contract.Type;
            return model;
        }
    }
    public class ApiScopePropertyMapper : AbstractMapper<PropertyContract, ApiScopeProperty>
    {
        public override PropertyContract ToContract(ApiScopeProperty model)
        {
            this.ThrowIfNull(model);
            return new PropertyContract
            {
                Key = model.Key,
                Value = model.Value
            };
        }

        public override ApiScopeProperty ToModel(PropertyContract contract)
        {
            return UpdateModel(new(), contract);
        }

        public override ApiScopeProperty UpdateModel(ApiScopeProperty model, PropertyContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Key = contract.Key;
            model.Value = contract.Value;
            return model;
        }
    }
}
