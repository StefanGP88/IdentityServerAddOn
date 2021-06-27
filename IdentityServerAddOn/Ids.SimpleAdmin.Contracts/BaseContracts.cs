using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ListDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }

    public class Identifiable<T>
    {
        public T Id { get; set; }
    }


    public class PropertyContract : Identifiable<int?>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ClaimsContract : Identifiable<int?>
    {
        public string Type { get; set; }
    }
    public class ValueClaimsContract : ClaimsContract
    {
        public string Value { get; set; }
    }
    public class ClientClaimsContract : ValueClaimsContract { }
    public class AspNetIdentityClaimsContract: ValueClaimsContract { }

    public class ScopeContract : Identifiable<int?>
    {
        public string Scope { get; set; }
    }

    public class SecretsContract : Identifiable<int?>
    {
        public int? ClientId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public SecretTypeEnum Type { get; set; }//TODO: make sure enum instead of int is not causing trouble in validators
        public DateTime Created { get; set; }
    }
    public class ClientSecretsContract : SecretsContract { }
    public class ApiResourceSecretsContract : SecretsContract { }
}
