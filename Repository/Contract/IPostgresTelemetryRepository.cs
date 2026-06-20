using Object;

namespace Repository.Contract
{
    public interface IPostgresTelemetryRepository
    {
        Task<List<AuditEvent>> GetAuditEvents();
    }
}
