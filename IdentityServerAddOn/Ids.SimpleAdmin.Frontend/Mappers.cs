using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend
{
    public static class Mappers
    {
        public static PagenationInfo ToPagenationInfo<T>(this ListDto<T> dto)
        {
            return new PagenationInfo
            {
                IsFirstPage = dto.Page == 0,
                IsLastPage = dto.Page == dto.GetLastPage(),
                Page = dto.Page,
                PageSize = dto.PageSize,
                TotalItems = dto.TotalItems,
                LastPage = dto.GetLastPage()
            };
        }

        private static int GetLastPage<T>(this ListDto<T> listDto)
        {
            var page = listDto.TotalItems / listDto.PageSize;
            if (listDto.TotalItems % listDto.PageSize > 0)
                return page;

            return --page;
        }

        public static int PageConverter(this PagenationInfo info, int newPageSize)
        {
            var currentItemIndex = info.Page * info.PageSize;
            return currentItemIndex / newPageSize;
        }

        public static string IsChecked(this bool b)
        {
            return b ? "checked": string.Empty;
        }
    }
}
