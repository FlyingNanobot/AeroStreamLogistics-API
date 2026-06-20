using Business.Contract;
using Object;
using Repository.Contract;

namespace Business.Concrete
{
    public class OpenSearchTelemetryService : IOpenSearchTelemetryService
    {
        private readonly IOpenSearchTelemetryRepository _repository;

        public OpenSearchTelemetryService(IOpenSearchTelemetryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProcessedState>> GetHistory(string flightId, DateTime from, DateTime to)
            => _repository.GetHistory(flightId, from, to);
    }
}
