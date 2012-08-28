using System;

namespace FubuDate.Domain
{
    public class Message
    {
        public string Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public DateTime Sent { get; set; }
    }
}