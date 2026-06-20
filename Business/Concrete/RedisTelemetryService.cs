using Business.Contract;
using Object;
using Repository.Contract;

namespace Business.Concrete
{
    public class RedisTelemetryService : IRedisTelemetryService
    {
        private readonly IRedisTelemetryRepository _repository;

        public RedisTelemetryService(IRedisTelemetryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<FlightTelemetry>> GetLiveTelemetry()
            => _repository.GetLiveTelemetry();
    }
}
