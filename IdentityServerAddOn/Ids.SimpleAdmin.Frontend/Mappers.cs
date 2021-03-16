using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            return b ? "checked" : string.Empty;
        }

        public static string ToLowerNoSpaces(this string s)
        {
            return s.Replace(" ", "").ToLower();
        }

        public static string FirstLetterToUpper(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s);
        }
        public static string AsTableBody(this string s)
        {
            return s + "TableBody";
        }

        public static bool IsNull(this object o)
        {
            return o is null;
        }
    }
}
