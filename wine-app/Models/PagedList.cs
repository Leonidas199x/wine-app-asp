using System;

namespace wine_app.Models
{
    public class PagedList<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public T Data { get; set; }

        public int PagesLeft = 2;

        public int PagesRight = 2;

        public int PreviousPage => Page - 1;

        public int NextPage => Page + 1;

        public int FromPage => Math.Max(1, Page - PagesLeft);

        public int ToPage => Math.Min(TotalPages, Page + PagesRight);

        public int FirstMiddlePage => (int)Math.Ceiling((double)(FromPage - 2) / 2) + 1;

        public int LastMiddlePage => (int)Math.Ceiling((double)(TotalPages - ToPage) / 2) + ToPage;
    }
}
