namespace PeakVenturesPixelService.lib
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken)
        {
            return _httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
        }
    }
}
