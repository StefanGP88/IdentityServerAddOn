using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiResourceValidator : EasyAdminValidatior<ApiResourceContract>
    {
        public ApiResourceValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Name).MaximumLength(200).MinimumLength(1).NotNull();
            RuleFor(x => x.DisplayName).MinimumLength(1).MaximumLength(200).NotNull();
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.AllowedAccessTokenSigningAlgorithms).MaximumLength(100);
            RuleForEach(x => x.Claims).SetValidator(new ClaimValidator(cache));
            RuleForEach(x => x.Scopes).SetValidator(new ScopeValidator(cache));
            RuleForEach(x => x.Secrets).SetValidator(new SecretValidator(cache));
            RuleForEach(x => x.Properties).SetValidator(new PropertyValidator(cache));
        }
    }
}
