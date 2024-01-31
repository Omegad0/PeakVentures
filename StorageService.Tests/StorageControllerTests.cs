using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PeakVenturesStorageService.Models;

namespace StorageService.Tests
{
    public class StorageControllerTests
    {
        [Fact]
        public async Task Store_Returns_OkResult()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["StorageFilePath"]).Returns(configuration["StorageFilePath"]);
            var controller = new StorageController(configurationMock.Object);

            // Act
            var result = await controller.Store(new TrackData { IpAddress = "127.0.0.1" });

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}