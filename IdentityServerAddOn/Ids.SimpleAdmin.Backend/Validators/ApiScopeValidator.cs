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
            RuleForEach(x => x.Claims).SetValidator(new ClaimValidator());
            RuleForEach(x => x.Properties).SetValidator(new PropertyValidator());
        }
    }
}
