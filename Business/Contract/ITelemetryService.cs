using System.Collections.Generic;
using Object.Models;

namespace Business.Contract
{
    public interface ITelemetryService
    {
        List<FlightTelemetry> GetLiveTelemetry();
    }
}
