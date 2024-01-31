
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using PeakVenturesPixelService.lib;
using System.Net;
using System.Net.Http.Json;

namespace PixelService.Tests
{
    public class PixelControllerTests
    {
        [Fact]
        public async Task Track_Returns_ImageFile()
        {
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json")
                .Build();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["StorageServiceUrl"]).Returns(configuration["StorageServiceUrl"]);



            var controller = new PixelController(httpClientWrapperMock.Object, configurationMock.Object);

            var httpContextMock = new Mock<HttpContext>();

            var httpRequestMock = new Mock<HttpRequest>();
            httpRequestMock.Setup(r => r.Headers)
                .Returns(new HeaderDictionary(new Dictionary<string, StringValues>
                {
                    {"Referer", "www.test.com"}
                }));

            httpContextMock.Setup(c => c.Connection.RemoteIpAddress)
                .Returns(IPAddress.Parse("1.1.1.1")); 

            httpContextMock.Setup(c => c.Request)
                .Returns(httpRequestMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            var result = await controller.Track();


            Assert.IsType<FileContentResult>(result);

        }
    }
}