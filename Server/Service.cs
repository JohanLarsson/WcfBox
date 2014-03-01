namespace DummyServer
{
    using System.Collections.Generic;
    using Contracts;

    public class Service : IService1
    {
        private readonly List<DummyMessage> _messages= new List<DummyMessage>(); 
        public void SendMessage(DummyMessage message)
        {
            _messages.Add(message);
        }
        public int GetMessageCount()
        {
            return _messages.Count;
        }
    }
}