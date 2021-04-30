using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidationFactory(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }


        public ValidationResult Validate<T>( T model)
        {
            if (model == null)
                return new ValidationResult();

            var validator = _contextAccessor.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
            var validationResult = validator.Validate(model);

            var dictionary = validationResult.Errors
                .Select(x => x.PropertyName)
                .ToDictionary(x => x, _ => new List<string>());

            foreach (var item in validationResult.Errors)
            {
                var message = item.ErrorMessage;
                dictionary[item.PropertyName].Add(message);
            }

            return new ValidationResult(dictionary);
        }
    }
}
