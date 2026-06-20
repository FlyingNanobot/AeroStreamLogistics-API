using Object;
using Repository.Contract;
using StackExchange.Redis;
using System.Text.Json;

namespace Repository.Concrete
{
    public class RedisTelemetryRepository : IRedisTelemetryRepository
    {
        private readonly string _connectionString;

        public RedisTelemetryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<FlightTelemetry>> GetLiveTelemetry()
        {
            var mux = await ConnectionMultiplexer.ConnectAsync(_connectionString);
            var db = mux.GetDatabase();
            var server = mux.GetServer(mux.GetEndPoints()[0]);

            var keys = server.Keys(pattern: "flight:*");
            var result = new List<FlightTelemetry>();

            foreach (var key in keys)
            {
                var json = await db.StringGetAsync(key);
                if (!json.IsNullOrEmpty)
                {
                    var telemetry = JsonSerializer.Deserialize<FlightTelemetry>((byte[])json!);
                    result.Add(telemetry);
                }
            }

            return result;
        }
    }
}
