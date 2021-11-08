using System.Collections.Generic;

namespace LogProxyApi.Models
{
    public class AirTableLogResponse
    {
        public List<LogRecordResponse> Records { get; set; }
        public string Offset { get; set; }
    }
}