using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;

namespace BusinessSolutions.Services.Ordering.API.Client.Base
{
    public class BaseClient : IBaseClient
    {
        private readonly string _baseUri;
        private readonly HttpClient _client;

        public BaseClient(HttpClient client, string baseUri)
        {
            _client = client;
            _baseUri = baseUri;
        }

        public async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync(uri, cancellationToken);
            return await ReadContentAs<T>(response);
        }

        public async Task<T> PostAsJson<T, U>(Uri uri, U data, CancellationToken cancellationToken)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync(uri, content, cancellationToken);

            return await ReadContentAs<T>(response);
        }

        public async Task<T> PutAsJson<T, U>(Uri uri, U data, CancellationToken cancellationToken)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync(uri, content, cancellationToken);

            return await ReadContentAs<T>(response);
        }

        public async Task DeleteAsync(Uri uri, CancellationToken cancellationToken)
        {
            await _client.DeleteAsync(uri, cancellationToken);
        }

        public async Task<T> ReadContentAs<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }

        public Uri BuildUri(string format, Dictionary<string, string>? dict = default)
        {
            var uri = new UriBuilder(_baseUri)
            {
                Path = format
            }.Uri;

            if (dict?.Count > 0)
            {
                foreach (var kvp in dict)
                {
                    uri = AddQuery(uri, kvp.Key, kvp.Value);
                }
            }
            return uri;
        }

        public Uri AddQuery(Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value.ToString());

            var ub = new UriBuilder(uri);
            ub.Query = httpValueCollection.ToString();

            return ub.Uri;
        }
    }
}