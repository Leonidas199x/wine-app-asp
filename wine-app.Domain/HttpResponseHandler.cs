using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wine_app.Domain
{
    public static class HttpResponseHandler
    {
        public static async Task<string> GetError(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseString);

            var errorMessage = string.Empty;

            foreach(var error in errors)
            {
                var errorString = string.Empty;

                foreach (var err in error.Value)
                {
                    errorString = string.Join(".", err);
                }

                errorMessage = string.IsNullOrEmpty(errorMessage) ? errorString : errorMessage + "," + errorString;
            }

            return errorMessage;
        }
    }
}
