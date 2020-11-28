using Ids.SimpleAdmin.Backend.Dtos;

namespace RazorTestLibrary
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
    }
}
