using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ClientsContract : Identifyable<int?>
    {
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ProtocolType { get; set; }
        public bool RequireClientSecret { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AlwaysIncludeUserClaimsInToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool RequireRequestObject { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelSessionRequired { get; set; }
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifeTime { get; set; }
        public int ConsentLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public bool EnabledLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime LastAccessed { get; set; }
        public int UserSsoLifetime { get; set; }
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; }
        public bool NonEditable { get; set; }
        public List<ClientScopeContract> Scopes { get; set; }
        public List<ClientSecretsContract> Secrets { get; set; }
        public List<ClientRedirectUriContract> RedirectUris { get; set; }
        public List<ClientPropertiesContract> Properties { get; set; }
        public List<ClientPostLogoutRedirectUrisContract> PostLogoutRedirectUris { get; set; }
        public List<ClientIdPRestrictionsContract> IdPRestrictions { get; set; }
        public List<ClientGrantTypesContract> GrantTypes { get; set; }
        public List<ClientCordOriginsContract> CordOrigins { get; set;}
        public List<ClientClaimsContract> Claims { get; set; }
    }
    public class ClientScopeContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Scope { get; set; }
    }
    public class ClientSecretsContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
    }
    public class ClientRedirectUriContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
    public class ClientPropertiesContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ClientPostLogoutRedirectUrisContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
    public class ClientIdPRestrictionsContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Provider { get; set; }
    }
    public class ClientGrantTypesContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string GrantType { get; set; }
    }
    public class ClientCordOriginsContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Origin { get; set; }
    }
    public class ClientClaimsContract : Identifyable<int?>
    {
        public int? ClientId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
