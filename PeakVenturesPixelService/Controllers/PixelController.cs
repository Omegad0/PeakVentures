using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PeakVenturesPixelService.lib;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class PixelController : ControllerBase
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IConfiguration _configuration;

    public PixelController(IHttpClientWrapper httpClientWrapper, IConfiguration configuration)
    {
        _httpClientWrapper = httpClientWrapper;
        _configuration = configuration;
    }

    [HttpGet("track")]
    public async Task<IActionResult> Track()
    {
        try
        {
            var referrer = Request.Headers["Referer"].ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var storageServiceUrl = _configuration["StorageServiceUrl"];

            var response = await _httpClientWrapper.PostAsJsonAsync($"{storageServiceUrl}/store", new
            {
                Referrer = string.IsNullOrEmpty(referrer) ? "" : referrer,
                UserAgent = string.IsNullOrEmpty(userAgent) ? "" : userAgent,
                IpAddress = ipAddress
            }, CancellationToken.None);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return File(Convert.FromBase64String("R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="), "image/gif");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
