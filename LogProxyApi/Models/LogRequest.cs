using System.Collections.Generic;

namespace LogProxyApi.Models
{
    public class LogRequest
    {
        public List<AirTableLogRecord> records { get; set; }
    }
}