using System;

namespace LogProxyApi.Models
{
    public class AirTableLogMessage
    {
        public string id { get; set; }
        public string Message { get; set; }
        public string Summary { get; set; }
        public string receivedAt { get; set; }
    }
}