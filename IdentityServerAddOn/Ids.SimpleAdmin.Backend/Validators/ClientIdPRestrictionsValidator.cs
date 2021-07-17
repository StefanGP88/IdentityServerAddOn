using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientIdPRestrictionsValidator : EasyAdminValidatior<ClientIdPRestrictionsContract>
    {
        public ClientIdPRestrictionsValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.Provider).MaximumLength(200).NotNull();
        }
    }
}
