using System;

namespace LogProxyApi.Models
{
    public class LogMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ReceivedAt { get; set; }
    }
}