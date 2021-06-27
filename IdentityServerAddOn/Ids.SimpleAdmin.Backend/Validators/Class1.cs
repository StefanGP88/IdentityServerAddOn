using FluentValidation;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class SecretValidator : AbstractValidator<SecretsContract>
    {
        public SecretValidator()
        {
            RuleFor(x => x.Description).MaximumLength(1000).When(x => x.GetType() == typeof(ApiResourceSecretsContract));
            RuleFor(x => x.Value).MaximumLength(4000).NotNull();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Created).NotNull();
        }
    }
    public class ClaimValidator : AbstractValidator<ClaimsContract>
    {
        public ClaimValidator()
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }
    public class ScopeValidator : AbstractValidator<ScopeContract>
    {
        public ScopeValidator()
        {
            RuleFor(x => x.Scope).MaximumLength(200).NotNull();
        }
    }
    public class PropertyValidator : AbstractValidator<PropertyContract>
    {
        public PropertyValidator()
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }

    public class AspNetIdentityClaimValidator : AbstractValidator<AspNetIdentityClaimsContract>
    {
    }
}
