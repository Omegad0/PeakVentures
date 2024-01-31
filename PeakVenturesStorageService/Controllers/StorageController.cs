
using Microsoft.AspNetCore.Mvc;
using PeakVenturesStorageService.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public StorageController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("store")]
    public async Task<IActionResult> Store([FromBody] TrackData visitData)
    {
        try
        {
            var storageFilePath = _configuration["StorageFilePath"];

            // Store in the append-only file
            var logEntry = $"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")}|{visitData.Referrer ?? "null"}|{visitData.UserAgent ?? "null"}|{visitData.IpAddress}";

            await System.IO.File.AppendAllTextAsync(storageFilePath, logEntry + Environment.NewLine);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}