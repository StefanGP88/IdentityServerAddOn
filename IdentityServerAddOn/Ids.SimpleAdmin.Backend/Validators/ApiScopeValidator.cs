using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiScopeValidator : AbstractValidator<ApiScopeContract>
    {
        public ApiScopeValidator()
        {
            RuleFor(x => x.Enabled).NotNull();
            RuleFor(x => x.Name).MaximumLength(200).NotNull();
            RuleFor(x => x.DisplayName).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Required).NotNull();
            RuleFor(x => x.Emphasize).NotNull();
            RuleFor(x => x.ShowInDiscoveryDocument).NotNull();
            RuleForEach(x => x.UserClaims).SetValidator(new ApiScopeClaimsValidator());
            RuleForEach(x => x.Properties).SetValidator(new ApiScopePropertiesValidator());
        }
    }
    public class ApiScopeClaimsValidator : AbstractValidator<ApiScopeClaimsContract>
    {
        public ApiScopeClaimsValidator()
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }
    public class ApiScopePropertiesValidator : AbstractValidator<ApiScopePropertiesContract>
    {
        public ApiScopePropertiesValidator()
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }
}
