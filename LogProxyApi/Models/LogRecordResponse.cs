using System;

namespace LogProxyApi.Models
{
    public class LogRecordResponse: AirTableLogRecord
    {
        public string Id { get; set; }
        public string createdTime { get; set; }
    }
}