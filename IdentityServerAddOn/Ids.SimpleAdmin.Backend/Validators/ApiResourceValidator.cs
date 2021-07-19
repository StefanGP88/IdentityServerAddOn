using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiResourceValidator : SimpleAdminValidatior<ApiResourceContract>
    {
        public ApiResourceValidator(ValidationCache cache,
            IValidator<ClaimsContract> claimValidator,
            IValidator<ScopeContract> scopeValidator,
            IValidator<ApiResourceSecretsContract> secretValidator,
            IValidator<PropertyContract> propertyValidator) : base(cache)
        {
            RuleFor(x => x.Name).MaximumLength(200).MinimumLength(1).NotNull();
            RuleFor(x => x.DisplayName).MinimumLength(1).MaximumLength(200).NotNull();
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.AllowedAccessTokenSigningAlgorithms).MaximumLength(100);
            RuleForEach(x => x.Claims).SetValidator(claimValidator);
            RuleForEach(x => x.Scopes).SetValidator(scopeValidator);
            RuleForEach(x => x.Secrets).SetValidator(secretValidator);
            RuleForEach(x => x.Properties).SetValidator(propertyValidator);
        }
    }
}
