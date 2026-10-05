using Microsoft.AspNetCore.Mvc;
using SRS.Application.DTOs.BiometricUserLog;
using SRS.Application.Interfaces.Service;

namespace SRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiometricUserLogsController : ControllerBase
    {
        private readonly IBiometricUserLogService _service;

        public BiometricUserLogsController(IBiometricUserLogService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] BiometricUserLogCreateDto model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return BadRequest(new
                {
                    message = "Invalid biometric log data."
                });
            }

            var result = await _service.InsertAsync(
                model,
                cancellationToken);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Unable to insert biometric log."
                });
            }

            return Ok(new
            {
                message = "Biometric log inserted successfully"
            });
        }
    }
}