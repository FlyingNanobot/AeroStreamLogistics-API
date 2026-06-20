namespace Object
{
    public class FlightTelemetry
    {
        // Unique flight identifier
        public string? FlightId { get; set; }

        // Aircraft details
        public string? AircraftType { get; set; }
        public string? TailNumber { get; set; }

        // Position data
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; } // in feet

        // Motion data
        public double Speed { get; set; }    // in knots
        public double Heading { get; set; }  // in degrees

        // Timing
        public DateTime Timestamp { get; set; }

        // Optional status fields
        public string? Status { get; set; }   // e.g. "En Route", "Landed", "Delayed"
    }
}
