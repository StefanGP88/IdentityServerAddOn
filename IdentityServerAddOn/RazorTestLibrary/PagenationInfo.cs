﻿namespace RazorTestLibrary
{
    public struct PagenationInfo
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public bool IsSecondPage { get; set; }
        public bool IsSecondLastPage { get; set; }
    }
}
