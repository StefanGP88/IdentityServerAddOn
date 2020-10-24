using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class ListDto<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
