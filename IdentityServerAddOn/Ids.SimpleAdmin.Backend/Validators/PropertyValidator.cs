using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class PropertyValidator : EasyAdminValidatior<PropertyContract>
    {
        public PropertyValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }
}
