using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class ListDto<T>
    {
        //TODO make sure Items and Total is not making unessarry dublicate db calls
        public List<T> Items { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
