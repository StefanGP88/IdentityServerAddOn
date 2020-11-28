using Ids.SimpleAdmin.Backend.Dtos;

namespace RazorTestLibrary
{
    public static class Mappers
    {
        public static PagenationInfo ToPagenationInfo<T>(this ListDto<T> dto)
        {
            return new PagenationInfo
            {
                IsFirstPage = dto.IsFirstPage,
                IsLastPage = dto.IsLastPage,
                IsSecondLastPage = dto.IsSecondLastPage,
                IsSecondPage = dto.IsSecondPage,
                Page = dto.Page,
                PageSize = dto.PageSize,
                TotalItems = dto.TotalItems,
                TotalPages = dto.TotalPages
            };
        }
    }
}
