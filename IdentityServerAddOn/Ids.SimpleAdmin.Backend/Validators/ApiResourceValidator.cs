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
            RuleForEach(x => x.UserClaims).SetValidator(new ApiResourceClaimsValidator());
            RuleForEach(x => x.Scopes).SetValidator(new ApiResourceScopesValidator());
            RuleForEach(x => x.Secrets).SetValidator(new ApiResourceSecretsValidator());
            RuleForEach(x => x.Properties).SetValidator(new ApiResourcePropertiesValidator());
        }
    }
    public class ApiResourceClaimsValidator : AbstractValidator<ApiResourceClaimsContract>
    {
        public ApiResourceClaimsValidator()
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }

    public class ApiResourcePropertiesValidator : AbstractValidator<PropertyContract>
    {
        public ApiResourcePropertiesValidator()
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }

    public class ApiResourceScopesValidator : AbstractValidator<ApiResourceScopesContract>
    {
        public ApiResourceScopesValidator()
        {
            RuleFor(x => x.Scope).MaximumLength(200).NotNull();
        }
    }

    public class ApiResourceSecretsValidator : AbstractValidator<ApiResourceSecretsContract>
    {
        public ApiResourceSecretsValidator()
        {
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Value).MaximumLength(4000).NotNull();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Created).NotNull();
        }
    }
}
