using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ApiResourceValidator : AbstractValidator<ApiResourceContract>
    {
        public ApiResourceValidator()
        {
            RuleFor(res => res.Name).MinimumLength(3).NotNull();
        }
    }
}
