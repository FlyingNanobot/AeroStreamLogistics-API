using Business.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Object;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IRedisTelemetryService _redisService;
        private readonly IOpenSearchTelemetryService _osService;
        private readonly IPostgresTelemetryService _pgService;
        private readonly IS3ArchiveService _s3Service;

        public DashboardController(
            IRedisTelemetryService redisService,
            IOpenSearchTelemetryService osService,
            IPostgresTelemetryService pgService,
            IS3ArchiveService s3Service)
        {
            _redisService = redisService;
            _osService = osService;
            _pgService = pgService;
            _s3Service = s3Service;
        }

        // Redis: Live telemetry
        [HttpGet("live")]
        public async Task<IActionResult> GetLive()
        {
            var data = await _redisService.GetLiveTelemetry();
            return Ok(new ApiResponse<List<FlightTelemetry>> { Success = true, Data = data });
        }

        // OpenSearch: Historical telemetry
        [HttpGet("history/{flightId}")]
        public async Task<IActionResult> GetHistory(string flightId, DateTime from, DateTime to)
        {
            var data = await _osService.GetHistory(flightId, from, to);
            return Ok(new ApiResponse<List<ProcessedState>> { Success = true, Data = data });
        }

        // Postgres: Audit events
        [HttpGet("audit")]
        public async Task<IActionResult> GetAudit()
        {
            var data = await _pgService.GetAuditEvents();
            return Ok(new ApiResponse<List<AuditEvent>> { Success = true, Data = data });
        }

        [HttpGet("archive/{key}")]
        public async Task<IActionResult> GetArchive(string key)
        {
            var data = await _s3Service.GetRawArchive(key);
            if (data == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Archive key '{key}' not found in bucket."
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Data = data
            });
        }
    }
}
