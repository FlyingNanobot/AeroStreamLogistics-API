using Npgsql;
using Object;
using Repository.Contract;

namespace Repository.Concrete
{
    public class PostgresTelemetryRepository : IPostgresTelemetryRepository
    {
        private readonly string _connString;

        public PostgresTelemetryRepository(string connString)
        {
            _connString = connString;
        }

        public async Task<List<AuditEvent>> GetAuditEvents()
        {
            var events = new List<AuditEvent>();

            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand("SELECT * FROM audit_events ORDER BY timestamp DESC", conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                events.Add(new AuditEvent
                {
                    EventId = reader.GetString(0),
                    FlightId = reader.GetString(1),
                    Message = reader.GetString(2),
                    Timestamp = reader.GetDateTime(3)
                });
            }

            return events;
        }
    }
}
