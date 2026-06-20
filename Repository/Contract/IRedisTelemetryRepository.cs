using Object;

namespace Repository.Contract
{
    public interface IRedisTelemetryRepository
    {
        Task<List<FlightTelemetry>> GetLiveTelemetry();
    }
}
