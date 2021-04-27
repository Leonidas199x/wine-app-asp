namespace wine_app.Models
{
    public class PagedList<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public T Data { get; set; }

        public int PreviousPage => Page - 1;

        public int NextPage => Page + 1;

        public int StartPage => GetStartPage(Page, TotalPages);

        public int EndPage => GetEndPage(Page, TotalPages);

        private int GetStartPage(int page, int totalPages)
        {
            if((page - 3) < 1)
            {
                return 1;
            }

            if(page > totalPages - 7)
            {
                return TotalPages - 7;
            }

            return Page - 3;
        }

        private int GetEndPage(int page, int totalPages)
        {
            if(page <= 4)
            {
                return 7;
            }

            if ((page + 3) > totalPages)
            {
                return totalPages;
            }

            if(page > totalPages - 7)
            {
                return totalPages;
            }

            return Page + 3;
        }
    }
}
