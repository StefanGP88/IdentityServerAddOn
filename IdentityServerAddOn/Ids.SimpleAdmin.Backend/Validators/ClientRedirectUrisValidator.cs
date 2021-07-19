﻿using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientRedirectUrisValidator : SimpleAdminValidatior<ClientRedirectUriContract>
    {
        public ClientRedirectUrisValidator(ValidationCache cache) : base(cache)
        {
            RuleFor(x => x.RedirectUri).MaximumLength(2000).NotNull();
        }
    }
}
