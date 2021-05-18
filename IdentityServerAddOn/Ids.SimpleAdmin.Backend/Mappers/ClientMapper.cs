using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public class ClientMapper : IMapper<ClientsContract, Client>
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

        public ClientsContract ToContract(Client m)
        {
            if (m is null) return null;
            return new ClientsContract
            {
                AbsoluteRefreshTokenLifetime = m.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = m.AccessTokenLifetime,
                AccessTokenType = m.AccessTokenType,
                AllowAccessTokensViaBrowser = m.AllowAccessTokensViaBrowser,
                AllowedIdentityTokenSigningAlgorithms = m.AllowedIdentityTokenSigningAlgorithms,
                AllowOfflineAccess = m.AllowOfflineAccess,
                AllowPlainTextPkce = m.AllowPlainTextPkce,
                AllowRememberConsent = m.AllowRememberConsent,
                AlwaysIncludeUserClaimsInToken = m.AlwaysIncludeUserClaimsInIdToken,
                AlwaysSendClientClaims = m.AlwaysSendClientClaims,
                AuthorizationCodeLifeTime = m.AuthorizationCodeLifetime,
                BackChannelLogoutSessionRequired = m.BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = m.BackChannelLogoutUri,
                ClientClaimsPrefix = m.ClientClaimsPrefix,
                ClientId = m.ClientId,
                ClientName = m.ClientName,
                ClientUri = m.ClientUri,
                ConsentLifetime = m.ConsentLifetime,
                Created = m.Created,
                Description = m.Description,
                DeviceCodeLifetime = m.DeviceCodeLifetime,
                Enabled = m.Enabled,
                EnabledLocalLogin = m.EnableLocalLogin,
                FrontChannelLogoutUri = m.FrontChannelLogoutUri,
                FrontChannelSessionRequired = m.FrontChannelLogoutSessionRequired,
                Id = m.Id,
                IdentityTokenLifetime = m.IdentityTokenLifetime,
                IncludeJwtId = m.IncludeJwtId,
                LastAccessed = m.LastAccessed,
                LogoUri = m.LogoUri,
                NonEditable = m.NonEditable,
                PairWiseSubjectSalt = m.PairWiseSubjectSalt,
                ProtocolType = m.ProtocolType,
                RefreshTokenExpiration = m.RefreshTokenExpiration,
                RefreshTokenUsage = m.RefreshTokenUsage,
                RequireClientSecret = m.RequireClientSecret,
                RequireConsent = m.RequireConsent,
                RequirePkce = m.RequirePkce,
                RequireRequestObject = m.RequireRequestObject,
                SlidingRefreshTokenLifetime = m.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = m.UpdateAccessTokenClaimsOnRefresh,
                Updated = m.Updated,
                UserCodeType = m.UserCodeType,
                UserSsoLifetime = m.UserSsoLifetime,
                Claims = m.Claims?.ConvertAll(x => _claim.ToContract(x)),
                CorsOrigins = m.AllowedCorsOrigins?.ConvertAll(x => _corsOrigin.ToContract(x)),
                GrantTypes = m.AllowedGrantTypes?.ConvertAll(x => _grantType.ToContract(x)),
                IdPRestrictions = m.IdentityProviderRestrictions?.ConvertAll(x => _idPRestriction.ToContract(x)),
                PostLogoutRedirectUris = m.PostLogoutRedirectUris?.ConvertAll(x => _postLogoutUri.ToContract(x)),
                Properties = m.Properties?.ConvertAll(x => _property.ToContract(x)),
                RedirectUris = m.RedirectUris?.ConvertAll(x => _redirectUri.ToContract(x)),
                Scopes = m.AllowedScopes?.ConvertAll(x => _scope.ToContract(x)),
                Secrets = m.ClientSecrets?.ConvertAll(x => _secret.ToContract(x)) //TODO: Consider making overload of ConvertAll that takes null list into account i.e. ConvertAllOrDefault or something
            };
        }

        public Client ToModel(ClientsContract dto)
        {
            if (dto == null) return null;
            return new Client
            {

                AbsoluteRefreshTokenLifetime = dto.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = dto.AccessTokenLifetime,
                AccessTokenType = dto.AccessTokenType,
                AllowAccessTokensViaBrowser = dto.AllowAccessTokensViaBrowser,
                AllowedIdentityTokenSigningAlgorithms = dto.AllowedIdentityTokenSigningAlgorithms,
                AllowOfflineAccess = dto.AllowOfflineAccess,
                AllowPlainTextPkce = dto.AllowPlainTextPkce,
                AllowRememberConsent = dto.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = dto.AlwaysIncludeUserClaimsInToken,
                AlwaysSendClientClaims = dto.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = dto.AuthorizationCodeLifeTime,
                BackChannelLogoutSessionRequired = dto.BackChannelLogoutSessionRequired,
                BackChannelLogoutUri = dto.BackChannelLogoutUri,
                ClientClaimsPrefix = dto.ClientClaimsPrefix,
                ClientId = dto.ClientId,
                ClientName = dto.ClientName,
                ClientUri = dto.ClientUri,
                ConsentLifetime = dto.ConsentLifetime,
                Created = dto.Created,
                Description = dto.Description,
                DeviceCodeLifetime = dto.DeviceCodeLifetime,
                Enabled = dto.Enabled,
                EnableLocalLogin = dto.EnabledLocalLogin,
                FrontChannelLogoutUri = dto.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = dto.FrontChannelSessionRequired,
                IdentityTokenLifetime = dto.IdentityTokenLifetime,
                IncludeJwtId = dto.IncludeJwtId,
                LastAccessed = dto.LastAccessed,
                LogoUri = dto.LogoUri,
                NonEditable = dto.NonEditable,
                PairWiseSubjectSalt = dto.PairWiseSubjectSalt,
                ProtocolType = dto.ProtocolType,
                RefreshTokenExpiration = dto.RefreshTokenExpiration,
                RefreshTokenUsage = dto.RefreshTokenUsage,
                RequireClientSecret = dto.RequireClientSecret,
                RequireConsent = dto.RequireConsent,
                RequirePkce = dto.RequirePkce,
                RequireRequestObject = dto.RequireRequestObject,
                SlidingRefreshTokenLifetime = dto.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = dto.UpdateAccessTokenClaimsOnRefresh,
                Updated = dto.Updated,
                UserCodeType = dto.UserCodeType,
                UserSsoLifetime = dto.UserSsoLifetime,
                Claims = dto.Claims?.ConvertAll(x => _claim.ToModel(x)),
                AllowedCorsOrigins = dto.CorsOrigins?.ConvertAll(x => _corsOrigin.ToModel(x)),
                AllowedGrantTypes = dto.GrantTypes?.ConvertAll(x => _grantType.ToModel(x)),
                IdentityProviderRestrictions = dto.IdPRestrictions?.ConvertAll(x => _idPRestriction.ToModel(x)),
                PostLogoutRedirectUris = dto.PostLogoutRedirectUris?.ConvertAll(x => _postLogoutUri.ToModel(x)),
                Properties = dto.Properties?.ConvertAll(x => _property.ToModel(x)),
                RedirectUris = dto.RedirectUris?.ConvertAll(x => _redirectUri.ToModel(x)),
                AllowedScopes = dto.Scopes?.ConvertAll(x => _scope.ToModel(x)),
                ClientSecrets = dto.Secrets?.ConvertAll(x => _secret.ToModel(x))
            };
        }

        public Client UpdateModel(Client model, ClientsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class IdPRestrictionMapper : IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction>
    {
        public ClientIdPRestrictionsContract ToContract(ClientIdPRestriction model)
        {
            throw new System.NotImplementedException();
        }

        public ClientIdPRestriction ToModel(ClientIdPRestrictionsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientIdPRestriction UpdateModel(ClientIdPRestriction model, ClientIdPRestrictionsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientClaimMapper : IMapper<ClientClaimsContract, ClientClaim>
    {
        public ClientClaimsContract ToContract(ClientClaim model)
        {
            throw new System.NotImplementedException();
        }

        public ClientClaim ToModel(ClientClaimsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientClaim UpdateModel(ClientClaim model, ClientClaimsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CorsOriginMapper : IMapper<ClientCorsOriginsContract, ClientCorsOrigin>
    {
        public ClientCorsOriginsContract ToContract(ClientCorsOrigin model)
        {
            throw new System.NotImplementedException();
        }

        public ClientCorsOrigin ToModel(ClientCorsOriginsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientCorsOrigin UpdateModel(ClientCorsOrigin model, ClientCorsOriginsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientPropertyMapper : IMapper<ClientPropertiesContract, ClientProperty>
    {
        public ClientPropertiesContract ToContract(ClientProperty model)
        {
            throw new System.NotImplementedException();
        }

        public ClientProperty ToModel(ClientPropertiesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientProperty UpdateModel(ClientProperty model, ClientPropertiesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientScopeMapper : IMapper<ClientScopeContract, ClientScope>
    {
        public ClientScopeContract ToContract(ClientScope model)
        {
            throw new System.NotImplementedException();
        }

        public ClientScope ToModel(ClientScopeContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientScope UpdateModel(ClientScope model, ClientScopeContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ClientSecretsMapper : IMapper<ClientSecretsContract, ClientSecret>
    {
        public ClientSecretsContract ToContract(ClientSecret model)
        {
            throw new System.NotImplementedException();
        }

        public ClientSecret ToModel(ClientSecretsContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientSecret UpdateModel(ClientSecret model, ClientSecretsContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class GrantTypeMapper : IMapper<ClientGrantTypesContract, ClientGrantType>
    {
        public ClientGrantTypesContract ToContract(ClientGrantType model)
        {
            throw new System.NotImplementedException();
        }

        public ClientGrantType ToModel(ClientGrantTypesContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientGrantType UpdateModel(ClientGrantType model, ClientGrantTypesContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class RedirectUriMapper : IMapper<ClientRedirectUriContract, ClientRedirectUri>
    {
        public ClientRedirectUriContract ToContract(ClientRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public ClientRedirectUri ToModel(ClientRedirectUriContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientRedirectUri UpdateModel(ClientRedirectUri model, ClientRedirectUriContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
    public class PostLogoutRedirectUri : IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>
    {
        public ClientPostLogoutRedirectUrisContract ToContract(ClientPostLogoutRedirectUri model)
        {
            throw new System.NotImplementedException();
        }

        public ClientPostLogoutRedirectUri ToModel(ClientPostLogoutRedirectUrisContract dto)
        {
            throw new System.NotImplementedException();
        }

        public ClientPostLogoutRedirectUri UpdateModel(ClientPostLogoutRedirectUri model, ClientPostLogoutRedirectUrisContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}
