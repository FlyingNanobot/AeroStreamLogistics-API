using Business.Contract;
using Object;
using Repository.Contract;

namespace Business.Concrete
{
    public class PostgresTelemetryService : IPostgresTelemetryService
    {
        private readonly IPostgresTelemetryRepository _repository;

        public PostgresTelemetryService(IPostgresTelemetryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AuditEvent>> GetAuditEvents()
            => _repository.GetAuditEvents();
    }

}
