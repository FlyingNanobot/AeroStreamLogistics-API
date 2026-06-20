namespace Object
{
    public class AuditEvent
    {
        public string EventId { get; set; }
        public string FlightId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
