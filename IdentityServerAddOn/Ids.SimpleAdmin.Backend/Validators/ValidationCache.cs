using FluentValidation.Results;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationCache
    {
        private Dictionary<object, ValidationResult> _cache;
        private Dictionary<string, ErrorSummary> _summary;
        public ValidationCache()
        {
            _cache = new Dictionary<object, ValidationResult>();
            _summary = new Dictionary<string, ErrorSummary>();
        }

        public void AddResult(object obj, ValidationResult result)
        {
            System.Console.WriteLine(obj.GetHashCode() + " added");
            _cache[obj] = result;
        }
        /*
         
        public bool HasErrors(object obj)
        {
            return !_cache[obj].IsValid;
        }

        public bool HasErrors(List<object> objs)
        {
            return objs.Any(x => !_cache[x].IsValid);
        }

        public int ErrorCount(object obj)
        {
            return _cache[obj].Errors.Count;
        }

        public int ErrorCount(List<object> objs)
        {
            return objs.Aggregate(0, (total, current) =>
            {
                total += _cache[current].Errors.Count;
                return total;
            });
        }

        public List<string> ErrorsMessages(object obj)
        {
            return _cache[obj].Errors.Select(x => x.ErrorMessage).ToList();
        }

        public string ErrorsMessage(object obj, string propertyName)
        {
            return _cache[obj].Errors
                .Where(x => x.PropertyName == propertyName)
                .Select(x => x.ErrorMessage)
                .First();
        }
         */
        public bool HasError(object obj, string[] propertyNames)
        {
            if (!_cache.ContainsKey(obj)) return false;
            foreach (var item in propertyNames)
            {
                if (_cache[obj].Errors.Where(x => x.PropertyName == item).Any())
                    return true;
            }
            return false;
        }
        public int HasCount(object obj, string[] propertyNames)
        {
            if (!_cache.ContainsKey(obj)) return 0;
            var count = 0;
            foreach (var item in propertyNames)
            {
                if (_cache[obj].Errors.Where(x => x.PropertyName == item).Any())
                    count++;
            }
            return count;
        }

        public ErrorSummary BaseSettingSummary(ClientsContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.ClientId),
                nameof(c.ClientName),
                nameof(c.Description),
                nameof(c.ProtocolType),
                nameof(c.AllowedIdentityTokenSigningAlgorithms),
                nameof(c.PairWiseSubjectSalt),
                nameof(c.UserCodeType),
                nameof(c.RefreshTokenUsage),
                nameof(c.RefreshTokenExpiration),
                nameof(c.AccessTokenType),
                nameof(c.Enabled),
                nameof(c.RequireConsent),
                nameof(c.AllowRememberConsent),
                nameof(c.RequirePkce),
                nameof(c.AllowPlainTextPkce),
                nameof(c.RequireRequestObject),
                nameof(c.AllowAccessTokensViaBrowser),
                nameof(c.FrontChannelSessionRequired),
                nameof(c.BackChannelLogoutSessionRequired),
                nameof(c.AllowOfflineAccess),
                nameof(c.EnabledLocalLogin),
                nameof(c.IncludeJwtId),
                nameof(c.NonEditable)
            };
            _summary["BaseSettings"] = CreateSummary(c,p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary CreateSummary(object obj, string[] propertyNames)
        {
            return new ErrorSummary
            {
                HasError = HasError(obj, propertyNames),
                ErrorCount = HasCount(obj, propertyNames)
            };
        }
    }
    public class ErrorSummary
    {
        public int ErrorCount { get; set; }
        public bool HasError { get; set; }
    }
}
