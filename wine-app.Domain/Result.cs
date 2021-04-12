using System.Net;

namespace wine_app.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }

        public Result() { }

        public Result(T data)
        {
            Data = data;
        }

        public Result(T data, bool isSuccess)
        {
            Data = data;
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string errors, T data) : this(data, isSuccess)
        {
            Error = errors;
        }

        public Result(string error, bool isSuccess, HttpStatusCode statusCode)
        {
            Error = error;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public Result() { }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(string errors, bool isSuccess) : this(isSuccess)
        {
            Error = errors;
        }

        public Result(string errors, bool isSuccess, HttpStatusCode statusCode) : this(errors, isSuccess)
        {
            HttpStatusCode = statusCode;
        }
    }
}
