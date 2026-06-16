using System.Collections.Generic;
using Business.Contract;
using Object.Models;

namespace Business.Concrete
{
    public class TelemetryService : ITelemetryService
    {
        private readonly Repository.Contract.ITelemetryRepository _repository;

        public TelemetryService(Repository.Contract.ITelemetryRepository repository)
        {
            _repository = repository;
        }

        public List<FlightTelemetry> GetLiveTelemetry()
        {
            return _repository.GetLiveTelemetry();
        }
    }
}
