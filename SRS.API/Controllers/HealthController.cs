using Microsoft.AspNetCore.Mvc;
namespace SRS.API.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        status = "Running",
        service = "SRS Biometric Sync API",
        message = "The API is running. Check application logs for background synchronization results."
    });
}