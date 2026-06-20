using Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contract
{
    public interface IOpenSearchTelemetryRepository
    {
        Task<List<ProcessedState>> GetHistory(string flightId, DateTime from, DateTime to);
    }
}
