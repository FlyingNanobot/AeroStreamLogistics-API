using System;
using System.Collections.Generic;

namespace Object.Models
{
    public class FlightTelemetry
    {
        public string FlightId { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int AltitudeFeet { get; set; }
        public int SpeedKnots { get; set; }
        public double CargoTemperatureCelsius { get; set; }
        public double FuelPercentage { get; set; }

        public FlightStatus Status { get; set; }
        public List<AnomalyEvent> ActiveAnomalies { get; set; } = new();
    }

    public enum FlightStatus
    {
        Normal,
        Warning,
        Critical
    }

    public class AnomalyEvent
    {
        public string EventId { get; set; }
        public string FlightId { get; set; }

        public AnomalySeverity Severity { get; set; }
        public string Message { get; set; }

        public DateTime TimestampUtc { get; set; }

        // Optional: threshold values for UI display
        public string Metric { get; set; }          // e.g., "Cargo Temp"
        public string CurrentValue { get; set; }    // e.g., "3.5°C"
        public string Threshold { get; set; }       // e.g., "2°C"
    }

    public enum AnomalySeverity
    {
        Normal,
        Warning,
        Critical
    }

    public class DashboardViewModel
    {
        public List<FlightTelemetry> LiveFlights { get; set; } = new();
        public List<AnomalyEvent> RecentAlerts { get; set; } = new();

        public OperatorInfo CurrentOperator { get; set; }
    }

    public class OperatorInfo
    {
        public string DisplayName { get; set; }
        public string Role { get; set; }            // logistics.ops, finance.ops, admin, superuser
        public string TokenIssuedAt { get; set; }
    }

    public class TelemetrySearchRequest
    {
        public string FlightId { get; set; }
        public bool OnlyWithAnomalies { get; set; }
    }

    public class TelemetrySearchResult
    {
        public FlightTelemetry Flight { get; set; }
        public List<AnomalyEvent> History { get; set; } = new();
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
