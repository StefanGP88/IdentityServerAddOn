using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ScopeValidator : EasyAdminValidatior<ScopeContract>
    {
        public ScopeValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Scope).MaximumLength(200).NotNull();
        }
    }
}
