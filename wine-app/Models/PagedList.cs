namespace wine_app.Models
{
    public class PagedList<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public T Data { get; set; }
    }
}
