﻿using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class IdentityResourceValidator : AbstractValidator<IdentityResourceContract>
    {
        public IdentityResourceValidator()
        {
            RuleFor(x => x.Enabled).NotNull();
            RuleFor(x => x.Name).MaximumLength(200).NotNull();
            RuleFor(x => x.DisplayName).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Required).NotNull();
            RuleFor(x => x.Emphasize).NotNull();
            RuleFor(x => x.ShowInDiscoveryDocument).NotNull();
            RuleFor(x => x.NonEditable).NotNull();
            RuleForEach(x => x.UserClaims).SetValidator(new IdentityResourceClaimsValidator());
            RuleForEach(x => x.Properties).SetValidator(new IdentityResourcePropertiesValidator());
        }
    }
    public class IdentityResourceClaimsValidator : AbstractValidator<IdentityResourceClaimsContract>
    {
        public IdentityResourceClaimsValidator()
        {
            RuleFor(x => x.Type).MaximumLength(200).NotNull();
        }
    }
    public class IdentityResourcePropertiesValidator : AbstractValidator<IdentityResourcePropertiesContract>
    {
        public IdentityResourcePropertiesValidator()
        {
            RuleFor(x => x.Key).MaximumLength(250).NotNull();
            RuleFor(x => x.Value).MaximumLength(2000).NotNull();
        }
    }
}
