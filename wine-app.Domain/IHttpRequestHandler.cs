using System.Net.Http;
using System.Threading.Tasks;

namespace wine_app.Domain
{
    public interface IHttpRequestHandler
    {
        Task<Result<T>> SendAsync<T>(HttpRequestMessage request);

        Task<Result> PostAsync(string url, StringContent body);

        Task<Result> PutAsync(string url, StringContent body);

        Task<Result> DeleteAsync(string url);
    }
}