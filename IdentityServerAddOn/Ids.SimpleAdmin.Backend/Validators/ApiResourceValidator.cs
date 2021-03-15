using FluentValidation;
using Ids.SimpleAdmin.Contracts;
using System.Collections.Generic;
using System.Linq;

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
        }
    }
    public class ApiResourceClaimsValidator : AbstractValidator<ApiResourceClaimsContract>
    {
        public ApiResourceClaimsValidator()
        {
            RuleFor(x => x.Type).MinimumLength(1).MaximumLength(200).NotNull();
        }

        public static ValidationResult GetValidationErrors(ApiResourceClaimsContract contract)
        {
            if (contract == null)
                return new ValidationResult();

            var validator = new ApiResourceClaimsValidator();
            var validationResult = validator.Validate(contract);

            var dictionary = validationResult.Errors
                .Select(x => x.PropertyName)
                .ToDictionary(x => x, _ => new List<string>());

            for (int i = 0; i < validationResult.Errors.Count; i++)
            {
                var mesage = validationResult.Errors[i].ErrorMessage;
                dictionary[validationResult.Errors[i].PropertyName].Add(mesage);
            }

            return new ValidationResult(dictionary);
        }
    }

    public class ApiResourcePropertiesValidator : AbstractValidator<ApiResourcePropertiesContract>
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
            RuleFor(x => x.Type).MaximumLength(250).NotNull();
            RuleFor(x => x.Created).NotNull();
        }
    }
    public class ValidationResult
    {
        private readonly Dictionary<string, List<string>> _result;
        public ValidationResult(Dictionary<string, List<string>> result)
        {
            _result = result;
        }
        public ValidationResult()
        {
            _result = new Dictionary<string, List<string>>();
        }

        public List<string> this[string key]
        {
            get
            {
                if (!_result.ContainsKey(key)) return null;
                return _result[key];
            }
        }
    }
}
