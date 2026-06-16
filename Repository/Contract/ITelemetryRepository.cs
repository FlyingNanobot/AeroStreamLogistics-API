using System.Collections.Generic;
using Object.Models;

namespace Repository.Contract
{
    public interface ITelemetryRepository
    {
        List<FlightTelemetry> GetLiveTelemetry();
    }
}
