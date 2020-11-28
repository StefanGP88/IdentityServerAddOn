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
        public int TotalPages => GetLastPage();
        public bool IsFirstPage => Page == 0;
        public bool IsLastPage => Page == GetLastPage();
        public bool IsSecondPage => Page == 1;
        public bool IsSecondLastPage => GetLastPage() - 1 == Page;
        private int GetLastPage()
        {
            var fullpages = TotalItems / PageSize;
            if (TotalItems % PageSize > 0)
                fullpages++;
            return fullpages;
        }
    }
}
