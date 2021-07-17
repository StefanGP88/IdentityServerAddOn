using FluentValidation.Results;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Html;
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

        public IHtmlContent ErrorMessage(object obj, string propertyName, out string errorClass)
        {
            errorClass = string.Empty;
            if (obj is null || !_cache.ContainsKey(obj)) return new HtmlString(string.Empty);

            var msg = _cache[obj].Errors
                .Where(x => x.PropertyName == propertyName || x.FormattedMessagePlaceholderValues["PropertyName"].ToString() == propertyName)
                .Select(x => x.ErrorMessage)
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(msg))
            {
                errorClass = " alert-danger ";
                return new HtmlString($" <small class=\"font-italic text-danger ml-2\">{msg}</small> ");
            }
            return new HtmlString(string.Empty);
        }
        public bool HasError(object obj, string[] propertyNames)
        {
            if (obj is null || !_cache.ContainsKey(obj)) return false;
            foreach (var item in propertyNames)
            {
                if (_cache[obj].Errors.Where(x => x.PropertyName == item || x.FormattedMessagePlaceholderValues["PropertyName"].ToString() == item).Any())
                    return true;
            }
            return false;
        }
        public int HasCount(object obj, string[] propertyNames)
        {
            var count = 0;
            if (obj is null || !_cache.ContainsKey(obj)) return count;

            foreach (var item in propertyNames)
            {
                if (_cache[obj].Errors.Where(x => x.PropertyName == item || x.FormattedMessagePlaceholderValues["PropertyName"].ToString() == item).Any())
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
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary BaseSettingSummary(ApiScopeContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.Name),
                nameof(c.DisplayName),
                nameof(c.Description),
                nameof(c.Enabled),
                nameof(c.Required),
                nameof(c.Emphasize),
                nameof(c.ShowInDiscoveryDocument)
            };
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary BaseSettingSummary(ApiResourceContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.Name),
                nameof(c.DisplayName),
                nameof(c.Description),
                nameof(c.AllowedAccessTokenSigningAlgorithms),
                nameof(c.Enabled),
                nameof(c.NonEditable),
                nameof(c.ShowInDiscoveryDocument)
            };
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary BaseSettingSummary(IdentityResourceContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.Name),
                nameof(c.DisplayName),
                nameof(c.Description),
                nameof(c.Enabled),
                nameof(c.Required),
                nameof(c.Emphasize),
                nameof(c.ShowInDiscoveryDocument),
                nameof(c.NonEditable)
            };
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary BaseSettingSummary(RolesContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.Name),
                nameof(c.NormalizedName)
            };
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary BaseSettingSummary(UserContract c)
        {
            if (_summary.ContainsKey("BaseSettings")) return _summary["BaseSettings"];
            var p = new[]
            {
                nameof(c.UserName),
                nameof(c.NormalizedUserName),
                nameof(c.Email),
                nameof(c.NormalizedEmail),
                nameof(c.SetPassword),
                nameof(c.PhoneNumber),
                nameof(c.EmailConfirmed),
                nameof(c.PhoneNumberConfirmed),
                nameof(c.TwoFactorEnabled),
                nameof(c.LockoutEnabled),
                nameof(c.LockoutEnd),
                nameof(c.AccessFailedCount)
            };
            _summary["BaseSettings"] = CreateSummary(c, p);
            return _summary["BaseSettings"];
        }
        public ErrorSummary LifeTimeSummary(ClientsContract c)
        {
            if (_summary.ContainsKey("LifeTime")) return _summary["LifeTime"];
            var p = new[]
            {
                nameof(c.UserSsoLifetime),
                nameof(c.DeviceCodeLifetime),
                nameof(c.IdentityTokenLifetime),
                nameof(c.AccessTokenLifetime),
                nameof(c.AuthorizationCodeLifeTime),
                nameof(c.ConsentLifetime),
                nameof(c.AbsoluteRefreshTokenLifetime),
                nameof(c.SlidingRefreshTokenLifetime)
            };
            _summary["LifeTime"] = CreateSummary(c, p);
            return _summary["LifeTime"];
        }
        public ErrorSummary UriSummary(ClientsContract c)
        {
            if (_summary.ContainsKey("Uri")) return _summary["Uri"];
            var p = new[]
            {
                nameof(c.ClientUri),
                nameof(c.LogoUri),
                nameof(c.FrontChannelLogoutUri),
                nameof(c.BackChannelLogoutUri)
            };
            var summary = CreateSummary(c, p);

            summary = c.RedirectUris.Aggregate(summary, (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.RedirectUri)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            summary = c.PostLogoutRedirectUris.Aggregate(summary, (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.PostLogoutRedirectUri)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            _summary["Uri"] = summary;

            return _summary["Uri"];
        }
        public ErrorSummary ClaimsSummary(ClientsContract c)
        {
            if (_summary.ContainsKey("Claims")) return _summary["Claims"];
            var prefix = new[]
            {
                nameof(c.ClientClaimsPrefix),
            };
            var summary = CreateSummary(c, prefix);


            summary = c.Claims.Aggregate(summary, (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.Type),
                    nameof(current.Value)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            _summary["Claims"] = summary;
            return _summary["Claims"];
        }
        public ErrorSummary ClaimsSummary(List<ClaimsContract> c)
        {
            if (_summary.ContainsKey("Claims")) return _summary["Claims"];

            var summary = c.Aggregate(new ErrorSummary(), (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.Type)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            _summary["Claims"] = summary;
            return _summary["Claims"];
        }
        public ErrorSummary CorsSummary(List<ClientCorsOriginsContract> c)
        {
            if (_summary.ContainsKey("Cors")) return _summary["Cors"];

            var summary = c.Aggregate(new ErrorSummary(), (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.Origin)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            _summary["Cors"] = summary;
            return _summary["Cors"];
        }
        public ErrorSummary GrantSummary(List<ClientGrantTypesContract> c)
        {
            if (_summary.ContainsKey("Grant")) return _summary["Grant"];

            var summary = c.Aggregate(new ErrorSummary(), (total, current) =>
            {
                var p = new[]
                {
                    nameof(current.GrantType)
                };
                var currentSummary = CreateSummary(current, p);
                total += currentSummary;

                return total;
            });

            _summary["Grant"] = summary;
            return _summary["Grant"];
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

        public static ErrorSummary operator +(ErrorSummary a, ErrorSummary b)
        {
            a.ErrorCount += b.ErrorCount;
            if (b.HasError)
                a.HasError = b.HasError;
            return a;
        }
    }
}
