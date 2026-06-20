using Object;

namespace Business.Contract
{
    public interface IOpenSearchTelemetryService
    {
        Task<List<ProcessedState>> GetHistory(string flightId, DateTime from, DateTime to);
    }
}
