using Object;

namespace Business.Contract
{
    public interface IPostgresTelemetryService
    {
        Task<List<AuditEvent>> GetAuditEvents();
    }
}
