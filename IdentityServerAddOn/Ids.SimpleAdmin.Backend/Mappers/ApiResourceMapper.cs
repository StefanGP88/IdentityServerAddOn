using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Linq;

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

    public class ApiResourceClaimsMapper : AbstractMapper<ApiResourceClaimsContract, ApiResourceClaim>
    {
        public override ApiResourceClaimsContract ToContract(ApiResourceClaim model)
        {
            this.ThrowIfNull(model);
            return new ApiResourceClaimsContract
            {
                Type = model.Type,
                ApiResourceId = model.ApiResourceId,
                Id = model.Id
            };
        }

        public override ApiResourceClaim ToModel(ApiResourceClaimsContract contract)
        {
            this.ThrowIfNull(contract);
            return UpdateModel(new(), contract);
        }

        public override ApiResourceClaim UpdateModel(ApiResourceClaim model, ApiResourceClaimsContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Type = contract.Type;
            return model;
        }
    }

    public class ApiResourcePropertiesMapper : AbstractMapper<ApiResourcePropertiesContract, ApiResourceProperty>
    {
        public override ApiResourcePropertiesContract ToContract(ApiResourceProperty model)
        {
            this.ThrowIfNull(model);
            return new ApiResourcePropertiesContract
            {
                Value = model.Value,
                Key = model.Key,
                Id = model.Id,
                ApiResourceId = model.ApiResourceId
            };
        }

        public override ApiResourceProperty ToModel(ApiResourcePropertiesContract contract)
        {
            this.ThrowIfNull(contract);
            return UpdateModel(new(), contract);
        }

        public override ApiResourceProperty UpdateModel(ApiResourceProperty model, ApiResourcePropertiesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Key = contract.Key;
            model.Value = contract.Value;
            return model;
        }
    }

    public class ApiResourceScopesMapper : AbstractMapper<ApiResourceScopesContract, ApiResourceScope>
    {
        public override ApiResourceScopesContract ToContract(ApiResourceScope model)
        {
            this.ThrowIfNull(model);
            return new ApiResourceScopesContract
            {
                Scope = model.Scope,
                ApiResourceId = model.ApiResourceId,
                Id = model.Id
            };
        }

        public override ApiResourceScope ToModel(ApiResourceScopesContract contract)
        {
            this.ThrowIfNull(contract);
            return UpdateModel(new(), contract);
        }

        public override ApiResourceScope UpdateModel(ApiResourceScope model, ApiResourceScopesContract contract)
        {
            this.ThrowIfNull(model, contract);
            model.Scope = contract.Scope;
            return model;
        }
    }

    public class ApiResourceSecretsMapper : AbstractMapper<ApiResourceSecretsContract, ApiResourceSecret>
    {

        private readonly ConfigurationDbContext _confContext;
        public ApiResourceSecretsMapper(ConfigurationDbContext confContext)
        {
            _confContext = confContext;
        }

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
                Type = SecretHelpers.GetSecretTypeEnum(model.Type, model.Value),
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
            model.Type = SecretHelpers.ConvertSecretTypeToString(contract.Type);
            model = UpdateSecretValue(model, contract);
            return model;
        }
        private ApiResourceSecret UpdateSecretValue(ApiResourceSecret model, ApiResourceSecretsContract contract)
        {
            if (!string.IsNullOrWhiteSpace(contract.Value)) return model;
            if (contract.Id is not null)
            {
                var isChanged = _confContext.ApiResources
                    .Where(x => x.Secrets.Any(y => y.Id == contract.Id && y.Value == contract.Value))
                    .Any();
                if (!isChanged) return model;
            }

            model.Value = SecretHelpers.GetHashedSecret(contract.Type, contract.Value);

            return model;
        }
    }
}
