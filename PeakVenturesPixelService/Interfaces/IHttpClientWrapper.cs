namespace PeakVenturesPixelService.lib
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value, CancellationToken cancellationToken);
    }
}