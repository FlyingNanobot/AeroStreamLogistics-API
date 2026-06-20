using Object;
using OpenSearch.Client;
using OpenSearch.Net;
using Repository.Contract;

namespace Repository.Concrete
{
    public class OpenSearchTelemetryRepository : IOpenSearchTelemetryRepository
    {
        private readonly IOpenSearchClient _client;

        public OpenSearchTelemetryRepository(string url)
        {
            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex("telemetry-history")
                .BasicAuthentication("admin", "admin")
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll);

            _client = new OpenSearchClient(settings);
        }

        public async Task<List<ProcessedState>> GetHistory(string flightId, DateTime from, DateTime to)
        {
            var response = await _client.SearchAsync<ProcessedState>(s => s
                .Index("telemetry-history")
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            m => m.Term(t => t.Field(f => f.FlightId).Value(flightId)),
                            m => m.DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(from)
                                .LessThanOrEquals(to)
                            )
                        )
                    )
                )
                .Size(5000)
            );

            return response.Documents.ToList();
        }
    }
}
