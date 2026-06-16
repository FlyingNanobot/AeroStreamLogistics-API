using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Business.Contract;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ITelemetryService _service;

        public DashboardController(ITelemetryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _service.GetLiveTelemetry();
            return Ok(new Object.Models.ApiResponse<List<Object.Models.FlightTelemetry>>
            {
                Success = true,
                Data = data
            });
        }
    }
}
