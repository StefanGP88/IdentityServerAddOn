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
}
