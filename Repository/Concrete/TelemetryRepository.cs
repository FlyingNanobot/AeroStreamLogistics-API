using System;
using System.Collections.Generic;
using Object.Models;

namespace Repository.Concrete
{
    public class TelemetryRepository : Contract.ITelemetryRepository
    {
        public List<FlightTelemetry> GetLiveTelemetry()
        {
            // return some dummy telemetry data for development / tests
            return new List<FlightTelemetry>
            {
                new FlightTelemetry
                {
                    FlightId = "ASL1001",
                    Airline = "AeroStream",
                    Origin = "JFK",
                    Destination = "LAX",
                    Latitude = 40.6413,
                    Longitude = -73.7781,
                    AltitudeFeet = 33000,
                    SpeedKnots = 450,
                    CargoTemperatureCelsius = 4.2,
                    FuelPercentage = 62.5,
                    Status = FlightStatus.Normal,
                },
                new FlightTelemetry
                {
                    FlightId = "ASL2002",
                    Airline = "AeroStream",
                    Origin = "LAX",
                    Destination = "SFO",
                    Latitude = 34.0522,
                    Longitude = -118.2437,
                    AltitudeFeet = 12000,
                    SpeedKnots = 300,
                    CargoTemperatureCelsius = 2.0,
                    FuelPercentage = 28.7,
                    Status = FlightStatus.Warning,
                    ActiveAnomalies = new List<AnomalyEvent>
                    {
                        new AnomalyEvent
                        {
                            EventId = "EVT-1",
                            FlightId = "ASL2002",
                            Severity = AnomalySeverity.Warning,
                            Message = "Cargo temperature approaching threshold",
                            TimestampUtc = DateTime.UtcNow.AddMinutes(-5),
                            Metric = "Cargo Temp",
                            CurrentValue = "2.0°C",
                            Threshold = "1.5°C"
                        }
                    }
                }
            };
        }
    }
}
