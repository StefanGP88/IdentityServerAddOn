using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiResourceValidator : AbstractValidator<ApiResourceContract>
    {
        public ApiResourceValidator()
        {
            RuleFor(x => x.Name).MaximumLength(200).MinimumLength(1).NotNull();
            RuleFor(x => x.DisplayName).MaximumLength(200).NotNull();
            RuleFor(x => x.Description).MaximumLength(1000).NotNull();
            RuleFor(x => x.AllowedAccessTokenSigningAlgorithms).MaximumLength(100);
            RuleForEach(x => x.UserClaims).SetValidator(new ApiResourceClaimsValidator());
        }
    }
    public class ApiResourceClaimsValidator: AbstractValidator<ApiResourceClaimsContract>
    {
        public ApiResourceClaimsValidator()
        {
            RuleFor(x => x.Type).MinimumLength(1).MaximumLength(200).NotNull();
        }
    }
}
