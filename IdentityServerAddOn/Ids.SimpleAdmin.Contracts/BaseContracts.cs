using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ListDto<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }

    public class Identifyable<T>
    {
#nullable enable
        public T? Id { get; set; }
#nullable disable
    }
}
