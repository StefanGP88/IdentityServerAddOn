using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiResourceValidator : AbstractValidator<ApiResourceContract>
    {
        public ApiResourceValidator()
        {
            RuleFor(x => x.Name).MaximumLength(200).MinimumLength(1).NotNull();
            RuleFor(x => x.DisplayName).MinimumLength(1).MaximumLength(200).NotNull();
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.AllowedAccessTokenSigningAlgorithms).MaximumLength(100);
            RuleForEach(x => x.UserClaims).SetValidator(new ClaimValidator());
            RuleForEach(x => x.Scopes).SetValidator(new ScopeValidator());
            RuleForEach(x => x.Secrets).SetValidator(new SecretValidator());
            RuleForEach(x => x.Properties).SetValidator(new PropertyValidator());
        }
    }
}
