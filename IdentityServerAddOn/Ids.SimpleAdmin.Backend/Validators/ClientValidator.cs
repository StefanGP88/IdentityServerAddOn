using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientValidator : AbstractValidator<ClientsContract>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Enabled).NotNull();
            RuleFor(x => x.ClientId).NotNull().MaximumLength(200);
            RuleFor(x => x.ProtocolType).NotNull().MaximumLength(200);
            RuleFor(x => x.RequireClientSecret).NotNull();
            RuleFor(x => x.ClientName).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.ClientUri).MaximumLength(2000);
            RuleFor(x => x.LogoUri).MaximumLength(2000);
            RuleFor(x => x.RequireConsent).NotNull();
            RuleFor(x => x.AllowRememberConsent).NotNull();
            RuleFor(x => x.AlwaysIncludeUserClaimsInToken).NotNull();
            RuleFor(x => x.RequirePkce).NotNull();
            RuleFor(x => x.AllowPlainTextPkce).NotNull();
            RuleFor(x => x.RequireRequestObject).NotNull();
            RuleFor(x => x.AllowAccessTokensViaBrowser).NotNull();
            RuleFor(x => x.FrontChannelLogoutUri).MaximumLength(2000);
            RuleFor(x => x.FrontChannelSessionRequired).NotNull();
            RuleFor(x => x.BackChannelLogoutUri).MaximumLength(2000);
            RuleFor(x => x.BackChannelLogoutSessionRequired).NotNull();
            RuleFor(x => x.AllowOfflineAccess).NotNull();
            RuleFor(x => x.IdentityTokenLifetime).NotNull();
            RuleFor(x => x.AllowedIdentityTokenSigningAlgorithms).MaximumLength(100);
            RuleFor(x => x.AccessTokenLifetime).NotNull();
            RuleFor(x => x.AuthorizationCodeLifeTime).NotNull();
            RuleFor(x => x.ConsentLifetime);
            RuleFor(x => x.AbsoluteRefreshTokenLifetime).NotNull();
            RuleFor(x => x.SlidingRefreshTokenLifetime).NotNull();
            RuleFor(x => x.RefreshTokenUsage).NotNull();
            RuleFor(x => x.UpdateAccessTokenClaimsOnRefresh).NotNull();
            RuleFor(x => x.RefreshTokenExpiration).NotNull();
            RuleFor(x => x.AccessTokenType).NotNull();
            RuleFor(x => x.EnabledLocalLogin).NotNull();
            RuleFor(x => x.IncludeJwtId).NotNull();
            RuleFor(x => x.AlwaysSendClientClaims).NotNull();
            RuleFor(x => x.ClientClaimsPrefix).MaximumLength(200);
            RuleFor(x => x.PairWiseSubjectSalt).MaximumLength(200);
            RuleFor(x => x.Created);
            RuleFor(x => x.Updated);
            RuleFor(x => x.LastAccessed);
            RuleFor(x => x.UserSsoLifetime);
            RuleFor(x => x.UserCodeType).MaximumLength(100);
            RuleFor(x => x.DeviceCodeLifetime).NotNull();
            RuleFor(x => x.NonEditable).NotNull();
        }
    }
}
