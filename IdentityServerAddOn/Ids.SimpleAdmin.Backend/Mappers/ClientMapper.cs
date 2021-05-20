using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ClientMapper : AbstractMapper<ClientsContract, Client>
    {
        private readonly IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> _idPRestriction;
        private readonly IMapper<ClientClaimsContract, ClientClaim> _claim;
        private readonly IMapper<ClientCorsOriginsContract, ClientCorsOrigin> _corsOrigin;
        private readonly IMapper<ClientPropertiesContract, ClientProperty> _property;
        private readonly IMapper<ClientScopeContract, ClientScope> _scope;
        private readonly IMapper<ClientSecretsContract, ClientSecret> _secret;
        private readonly IMapper<ClientGrantTypesContract, ClientGrantType> _grantType;
        private readonly IMapper<ClientRedirectUriContract, ClientRedirectUri> _redirectUri;
        private readonly IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> _postLogoutUri;


        public ClientMapper(IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction> idPRestriction,
          IMapper<ClientClaimsContract, ClientClaim> claim,
          IMapper<ClientCorsOriginsContract, ClientCorsOrigin> corsOrigin,
          IMapper<ClientPropertiesContract, ClientProperty> property,
          IMapper<ClientScopeContract, ClientScope> scope,
          IMapper<ClientSecretsContract, ClientSecret> secret,
          IMapper<ClientGrantTypesContract, ClientGrantType> grantType,
          IMapper<ClientRedirectUriContract, ClientRedirectUri> redirectUri,
          IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri> postLogoutUri)
        {
            _idPRestriction = idPRestriction;
            _claim = claim;
            _corsOrigin = corsOrigin;
            _property = property;
            _scope = scope;
            _secret = secret;
            _grantType = grantType;
            _redirectUri = redirectUri;
            _postLogoutUri = postLogoutUri;
        }

        public override ClientsContract ToContract(Client model)
        {
            if (model is null) return null;
            return new ClientsContract
            {
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = model.AccessTokenLifetime,
                AccessTokenType = model.AccessTokenType,
                AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser,
                AllowedIdentityTokenSigningAlgorithms = model.AllowedIdentityTokenSigningAlgorithms,
                AllowOfflineAccess = model.AllowOfflineAccess,
                AllowPlainTextPkce = model.AllowPlainTextPkce,
                AllowRememberConsent = model.AllowRememberConsent,
                AlwaysIncludeUserClaimsInToken = model.AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = model.AlwaysSendClientClaims,
                AuthorizationCodeLifeTime = model.AuthorizationCodeLifetime,
                BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = model.BackChannelLogoutUri,
                ClientClaimsPrefix = model.ClientClaimsPrefix,
                ClientId = model.ClientId,
                ClientName = model.ClientName,
                ClientUri = model.ClientUri,
                ConsentLifetime = model.ConsentLifetime,
                Created = model.Created,
                Description = model.Description,
                DeviceCodeLifetime = model.DeviceCodeLifetime,
                Enabled = model.Enabled,
                EnabledLocalLogin = model.EnableLocalLogin,
                FrontChannelLogoutUri = model.FrontChannelLogoutUri,
                FrontChannelSessionRequired = model.FrontChannelLogoutSessionRequired,
                Id = model.Id,
                IdentityTokenLifetime = model.IdentityTokenLifetime,
                IncludeJwtId = model.IncludeJwtId,
                LastAccessed = model.LastAccessed,
                LogoUri = model.LogoUri,
                NonEditable = model.NonEditable,
                PairWiseSubjectSalt = model.PairWiseSubjectSalt,
                ProtocolType = model.ProtocolType,
                RefreshTokenExpiration = model.RefreshTokenExpiration,
                RefreshTokenUsage = model.RefreshTokenUsage,
                RequireClientSecret = model.RequireClientSecret,
                RequireConsent = model.RequireConsent,
                RequirePkce = model.RequirePkce,
                RequireRequestObject = model.RequireRequestObject,
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh,
                Updated = model.Updated,
                UserCodeType = model.UserCodeType,
                UserSsoLifetime = model.UserSsoLifetime,
                Claims = model.Claims?.ConvertAll(x => _claim.ToContract(x)),
                CorsOrigins = model.AllowedCorsOrigins?.ConvertAll(x => _corsOrigin.ToContract(x)),
                GrantTypes = model.AllowedGrantTypes?.ConvertAll(x => _grantType.ToContract(x)),
                IdPRestrictions = model.IdentityProviderRestrictions?.ConvertAll(x => _idPRestriction.ToContract(x)),
                PostLogoutRedirectUris = model.PostLogoutRedirectUris?.ConvertAll(x => _postLogoutUri.ToContract(x)),
                Properties = model.Properties?.ConvertAll(x => _property.ToContract(x)),
                RedirectUris = model.RedirectUris?.ConvertAll(x => _redirectUri.ToContract(x)),
                Scopes = model.AllowedScopes?.ConvertAll(x => _scope.ToContract(x)),
                Secrets = model.ClientSecrets?.ConvertAll(x => _secret.ToContract(x)) //TODO: Consider making overload of ConvertAll that takes null list into account i.e. ConvertAllOrDefault or something
            };
        }

        public override Client ToModel(ClientsContract contract)
        {
            if (contract == null) return null;
            return new Client
            {

                AbsoluteRefreshTokenLifetime = contract.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = contract.AccessTokenLifetime,
                AccessTokenType = contract.AccessTokenType,
                AllowAccessTokensViaBrowser = contract.AllowAccessTokensViaBrowser,
                AllowedIdentityTokenSigningAlgorithms = contract.AllowedIdentityTokenSigningAlgorithms,
                AllowOfflineAccess = contract.AllowOfflineAccess,
                AllowPlainTextPkce = contract.AllowPlainTextPkce,
                AllowRememberConsent = contract.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = contract.AlwaysIncludeUserClaimsInToken,
                AlwaysSendClientClaims = contract.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = contract.AuthorizationCodeLifeTime,
                BackChannelLogoutSessionRequired = contract.BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = contract.BackChannelLogoutUri,
                ClientClaimsPrefix = contract.ClientClaimsPrefix,
                ClientId = contract.ClientId,
                ClientName = contract.ClientName,
                ClientUri = contract.ClientUri,
                ConsentLifetime = contract.ConsentLifetime,
                Created = contract.Created,
                Description = contract.Description,
                DeviceCodeLifetime = contract.DeviceCodeLifetime,
                Enabled = contract.Enabled,
                EnableLocalLogin = contract.EnabledLocalLogin,
                FrontChannelLogoutUri = contract.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = contract.FrontChannelSessionRequired,
                IdentityTokenLifetime = contract.IdentityTokenLifetime,
                IncludeJwtId = contract.IncludeJwtId,
                LastAccessed = contract.LastAccessed,
                LogoUri = contract.LogoUri,
                NonEditable = contract.NonEditable,
                PairWiseSubjectSalt = contract.PairWiseSubjectSalt,
                ProtocolType = contract.ProtocolType,
                RefreshTokenExpiration = contract.RefreshTokenExpiration,
                RefreshTokenUsage = contract.RefreshTokenUsage,
                RequireClientSecret = contract.RequireClientSecret,
                RequireConsent = contract.RequireConsent,
                RequirePkce = contract.RequirePkce,
                RequireRequestObject = contract.RequireRequestObject,
                SlidingRefreshTokenLifetime = contract.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = contract.UpdateAccessTokenClaimsOnRefresh,
                Updated = contract.Updated,
                UserCodeType = contract.UserCodeType,
                UserSsoLifetime = contract.UserSsoLifetime,
                Claims = contract.Claims?.ConvertAll(x => _claim.ToModel(x)),
                AllowedCorsOrigins = contract.CorsOrigins?.ConvertAll(x => _corsOrigin.ToModel(x)),
                AllowedGrantTypes = contract.GrantTypes?.ConvertAll(x => _grantType.ToModel(x)),
                IdentityProviderRestrictions = contract.IdPRestrictions?.ConvertAll(x => _idPRestriction.ToModel(x)),
                PostLogoutRedirectUris = contract.PostLogoutRedirectUris?.ConvertAll(x => _postLogoutUri.ToModel(x)),
                Properties = contract.Properties?.ConvertAll(x => _property.ToModel(x)),
                RedirectUris = contract.RedirectUris?.ConvertAll(x => _redirectUri.ToModel(x)),
                AllowedScopes = contract.Scopes?.ConvertAll(x => _scope.ToModel(x)),
                ClientSecrets = contract.Secrets?.ConvertAll(x => _secret.ToModel(x))
            };
        }

        public override Client UpdateModel(Client model, ClientsContract contract)
        {
            if (model is null && contract is null) return null;
            if (model is null) return ToModel(contract);
            if (contract is null) return model;

            model.AbsoluteRefreshTokenLifetime = contract.AbsoluteRefreshTokenLifetime;
            model.AccessTokenLifetime = contract.AccessTokenLifetime;
            model.AccessTokenType = contract.AccessTokenType;
            model.AllowAccessTokensViaBrowser = contract.AllowAccessTokensViaBrowser;
            model.AllowedIdentityTokenSigningAlgorithms = contract.AllowedIdentityTokenSigningAlgorithms;
            model.AllowOfflineAccess = contract.AllowOfflineAccess;
            model.AllowPlainTextPkce = contract.AllowPlainTextPkce;
            model.AllowRememberConsent = contract.AllowRememberConsent;
            model.AlwaysIncludeUserClaimsInIdToken = contract.AlwaysIncludeUserClaimsInToken;
            model.AlwaysSendClientClaims = contract.AlwaysSendClientClaims;
            model.AuthorizationCodeLifetime = contract.AuthorizationCodeLifeTime;
            model.BackChannelLogoutSessionRequired = contract.BackChannelLogoutSessionRequired;
            model.BackChannelLogoutUri = contract.BackChannelLogoutUri;
            model.ClientClaimsPrefix = contract.ClientClaimsPrefix;
            model.ClientId = contract.ClientId;
            model.ClientName = contract.ClientName;
            model.ClientUri = contract.ClientUri;
            model.ConsentLifetime = contract.ConsentLifetime;
            model.Created = contract.Created;
            model.Description = contract.Description;
            model.DeviceCodeLifetime = contract.DeviceCodeLifetime;
            model.Enabled = contract.Enabled;
            model.EnableLocalLogin = contract.EnabledLocalLogin;
            model.FrontChannelLogoutUri = contract.FrontChannelLogoutUri;
            model.FrontChannelLogoutSessionRequired = contract.FrontChannelSessionRequired;
            model.IdentityTokenLifetime = contract.IdentityTokenLifetime;
            model.IncludeJwtId = contract.IncludeJwtId;
            model.LastAccessed = contract.LastAccessed;
            model.LogoUri = contract.LogoUri;
            model.NonEditable = contract.NonEditable;
            model.PairWiseSubjectSalt = contract.PairWiseSubjectSalt;
            model.ProtocolType = contract.ProtocolType;
            model.RefreshTokenExpiration = contract.RefreshTokenExpiration;
            model.RefreshTokenUsage = contract.RefreshTokenUsage;
            model.RequireClientSecret = contract.RequireClientSecret;
            model.RequireConsent = contract.RequireConsent;
            model.RequirePkce = contract.RequirePkce;
            model.RequireRequestObject = contract.RequireRequestObject;
            model.SlidingRefreshTokenLifetime = contract.SlidingRefreshTokenLifetime;
            model.UpdateAccessTokenClaimsOnRefresh = contract.UpdateAccessTokenClaimsOnRefresh;
            model.Updated = contract.Updated;
            model.UserCodeType = contract.UserCodeType;
            model.UserSsoLifetime = contract.UserSsoLifetime;

            model.Claims =  _claim.UpdateList(model.Claims, contract.Claims);
            model.AllowedCorsOrigins = _corsOrigin.UpdateList(model.AllowedCorsOrigins , contract.CorsOrigins);
            model.AllowedGrantTypes = _grantType.UpdateList(model.AllowedGrantTypes , contract.GrantTypes);
            model.IdentityProviderRestrictions = _idPRestriction.UpdateList(model.IdentityProviderRestrictions ,contract.IdPRestrictions);
            model.PostLogoutRedirectUris = _postLogoutUri.UpdateList(model.PostLogoutRedirectUris , contract.PostLogoutRedirectUris);
            model.Properties = _property.UpdateList(model.Properties , contract.Properties);
            model.RedirectUris = _redirectUri.UpdateList(model.RedirectUris , contract.RedirectUris);
            model.AllowedScopes = _scope.UpdateList(model.AllowedScopes ,contract.Scopes);
            model.ClientSecrets = _secret.UpdateList(model.ClientSecrets , contract.Secrets);

            return model;
        }
    }
    public class IdPRestrictionMapper : AbstractMapper<ClientIdPRestrictionsContract, ClientIdPRestriction>
    {
        public override ClientIdPRestrictionsContract ToContract(ClientIdPRestriction model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientIdPRestriction ToModel(ClientIdPRestrictionsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientIdPRestriction UpdateModel(ClientIdPRestriction model, ClientIdPRestrictionsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientClaimMapper : AbstractMapper<ClientClaimsContract, ClientClaim>
    {
        public override ClientClaimsContract ToContract(ClientClaim model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientClaim ToModel(ClientClaimsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientClaim UpdateModel(ClientClaim model, ClientClaimsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CorsOriginMapper : AbstractMapper<ClientCorsOriginsContract, ClientCorsOrigin>
    {
        public override ClientCorsOriginsContract ToContract(ClientCorsOrigin model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientCorsOrigin ToModel(ClientCorsOriginsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientCorsOrigin UpdateModel(ClientCorsOrigin model, ClientCorsOriginsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientPropertyMapper : AbstractMapper<ClientPropertiesContract, ClientProperty>
    {
        public override ClientPropertiesContract ToContract(ClientProperty model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientProperty ToModel(ClientPropertiesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientProperty UpdateModel(ClientProperty model, ClientPropertiesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientScopeMapper : AbstractMapper<ClientScopeContract, ClientScope>
    {
        public override ClientScopeContract ToContract(ClientScope model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientScope ToModel(ClientScopeContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientScope UpdateModel(ClientScope model, ClientScopeContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientSecretsMapper : AbstractMapper<ClientSecretsContract, ClientSecret>
    {
        public override ClientSecretsContract ToContract(ClientSecret model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientSecret ToModel(ClientSecretsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientSecret UpdateModel(ClientSecret model, ClientSecretsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class GrantTypeMapper : AbstractMapper<ClientGrantTypesContract, ClientGrantType>
    {
        public override ClientGrantTypesContract ToContract(ClientGrantType model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientGrantType ToModel(ClientGrantTypesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientGrantType UpdateModel(ClientGrantType model, ClientGrantTypesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class RedirectUriMapper : AbstractMapper<ClientRedirectUriContract, ClientRedirectUri>
    {
        public override ClientRedirectUriContract ToContract(ClientRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientRedirectUri ToModel(ClientRedirectUriContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientRedirectUri UpdateModel(ClientRedirectUri model, ClientRedirectUriContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class PostLogoutRedirectUri : AbstractMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>
    {
        public override ClientPostLogoutRedirectUrisContract ToContract(ClientPostLogoutRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public override ClientPostLogoutRedirectUri ToModel(ClientPostLogoutRedirectUrisContract dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClientPostLogoutRedirectUri UpdateModel(ClientPostLogoutRedirectUri model, ClientPostLogoutRedirectUrisContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}
