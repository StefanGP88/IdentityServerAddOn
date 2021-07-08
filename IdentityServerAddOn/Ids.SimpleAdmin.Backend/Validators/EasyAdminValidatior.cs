using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class EasyAdminValidatior<T> : AbstractValidator<T>
    {
        private readonly ValidationCache _cache;
        public EasyAdminValidatior(ValidationCache validationCache)
        {
            _cache = validationCache;
        }
        protected override bool PreValidate(ValidationContext<T> context, FluentValidation.Results.ValidationResult result)
        {
            _cache.AddResult(context.InstanceToValidate, result);
            return base.PreValidate(context, result);
        }
    }
}
