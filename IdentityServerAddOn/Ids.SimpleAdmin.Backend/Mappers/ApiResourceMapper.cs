using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ApiResourceMapper : AbstractMapper<ApiResourceContract, ApiResource>
    {

        private readonly IMapper<ApiResourceClaimsContract, ApiResourceClaim> _claim;
        private readonly IMapper<ApiResourcePropertiesContract, ApiResourceProperty> _property;
        private readonly IMapper<ApiResourceScopesContract, ApiResourceScope> _scope;
        private readonly IMapper<ApiResourceSecretsContract, ApiResourceSecret> _secret;

        public ApiResourceMapper(IMapper<ApiResourceClaimsContract, ApiResourceClaim> claimMapper,
            IMapper<ApiResourcePropertiesContract, ApiResourceProperty> propertyMapper,
            IMapper<ApiResourceScopesContract, ApiResourceScope> scopeMapper,
            IMapper<ApiResourceSecretsContract, ApiResourceSecret> secretMapper)
        {
            _claim = claimMapper;
            _property = propertyMapper;
            _scope = scopeMapper;
            _secret = secretMapper;
        }
        public override ApiResourceContract ToContract(ApiResource model)
        {
            this.ThrowIfNull(model);
            return new ApiResourceContract
            {
                AllowedAccessTokenSigningAlgorithms = model.AllowedAccessTokenSigningAlgorithms,
                Created = model.Created,
                Description = model.Description,
                DisplayName = model.DisplayName,
                Enabled = model.Enabled,
                Id = model.Id,
                LastAccessed = model.LastAccessed,
                Name = model.Name,
                NonEditable = model.NonEditable,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                Updated = model.Updated,
                Properties = model.Properties?.ConvertAll(_property.ToContract),
                Scopes = model.Scopes?.ConvertAll(_scope.ToContract),
                Secrets = model.Secrets?.ConvertAll(_secret.ToContract),
                UserClaims = model.UserClaims?.ConvertAll(_claim.ToContract)
            };
        }
        public override ApiResource ToModel(ApiResourceContract contract)
        {
            this.ThrowIfNull(contract);
            var model = UpdateModel(new(), contract);
            model.Created = DateTime.UtcNow;
            return model;
        }
        public override ApiResource UpdateModel(ApiResource model, ApiResourceContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.AllowedAccessTokenSigningAlgorithms = contract.AllowedAccessTokenSigningAlgorithms;
            model.Description = contract.Description;
            model.DisplayName = contract.DisplayName;
            model.Enabled = contract.Enabled;
            model.LastAccessed = contract.LastAccessed;
            model.Name = contract.Name;
            model.NonEditable = contract.NonEditable;
            model.ShowInDiscoveryDocument = contract.ShowInDiscoveryDocument;
            model.Updated = DateTime.UtcNow;
            model.Properties = contract.Properties?.ConvertAll(_property.ToModel);
            model.Scopes = contract.Scopes?.ConvertAll(_scope.ToModel);
            model.Secrets = contract.Secrets?.ConvertAll(_secret.ToModel);
            model.UserClaims = contract.UserClaims?.ConvertAll(_claim.ToModel);
            return model;
        }
    }

    public class ApiResourceClaimsMapper : AbstractMapper<ApiResourceClaim, ApiResourceClaimsContract>
    {
        public override ApiResourceClaim ToContract(ApiResourceClaimsContract model)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourceClaimsContract ToModel(ApiResourceClaim contract)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourceClaimsContract UpdateModel(ApiResourceClaimsContract model, ApiResourceClaim contract)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ApiResourcePropertiesMapper : AbstractMapper<ApiResourceProperty, ApiResourcePropertiesContract>
    {
        public override ApiResourceProperty ToContract(ApiResourcePropertiesContract model)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourcePropertiesContract ToModel(ApiResourceProperty contract)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourcePropertiesContract UpdateModel(ApiResourcePropertiesContract model, ApiResourceProperty contract)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ApiResourceScopesMapper : AbstractMapper<ApiResourceScope, ApiResourceScopesContract>
    {
        public override ApiResourceScope ToContract(ApiResourceScopesContract model)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourceScopesContract ToModel(ApiResourceScope contract)
        {
            throw new System.NotImplementedException();
        }
        public override ApiResourceScopesContract UpdateModel(ApiResourceScopesContract model, ApiResourceScope contract)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ApiResourceSecretsMapper : AbstractMapper<ApiResourceSecretsContract, ApiResourceSecret>
    {
        public override ApiResourceSecretsContract ToContract(ApiResourceSecret model)
        {
            this.ThrowIfNull(model);
            return new ApiResourceSecretsContract
            {
                ApiResourceId = model.ApiResourceId,
                Created = model.Created,
                Description = model.Description,
                Expiration = model.Expiration,
                Id = model.Id,
                Type = model.Type,
                Value = model.Value
            };
        }
        public override ApiResourceSecret ToModel(ApiResourceSecretsContract contract)
        {
            this.ThrowIfNull(contract);
            var model = UpdateModel(new(), contract);
            model.Created = DateTime.UtcNow;
            return model;
        }
        public override ApiResourceSecret UpdateModel(ApiResourceSecret model, ApiResourceSecretsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Description = contract.Description;
            model.Expiration = contract.Expiration;
            model.Type = contract.Type;
            model.Value = contract.Value;
            return model;
        }
    }
}
