using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private Dictionary<object, ValidationResult> _cache;
        public ValidationFactory(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _cache = new Dictionary<object, ValidationResult>();
        }

        public ValidationResult Validate<T>(T model)
        {
            if (model == null)
                return new ValidationResult();

            if (_cache.ContainsKey(model))
                return _cache[model];

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

            var result = new ValidationResult(dictionary);
            return result;
        }
        public string this[params string[] s]
        {
            get { return ""; }
        }
    }
}
