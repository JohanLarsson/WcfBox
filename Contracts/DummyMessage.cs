namespace Contracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DummyMessage
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
