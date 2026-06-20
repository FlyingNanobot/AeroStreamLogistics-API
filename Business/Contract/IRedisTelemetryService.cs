using Object;

namespace Business.Contract
{
    public interface IRedisTelemetryService
    {
        Task<List<FlightTelemetry>> GetLiveTelemetry();
    }
}
