namespace wine_app.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public T Data { get; set; }

        public Result() { }

        public Result(bool isSuccess, string errors, T data)
        {
            IsSuccess = isSuccess;
            Error = errors;
            Data = data;
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public Result() { }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(string errors, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Error = errors;
        }
    }
}
