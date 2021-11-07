using System.Collections.Generic;

namespace LogProxyApi.Models
{
    public class LogResponse
    {
        public List<LogRecordResponse> Records { get; set; }
    }
}