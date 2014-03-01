
namespace DummyServer
{
    using System.Collections.Generic;
    using Contracts;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static readonly List<DummyMessage> Messages = new List<DummyMessage>();
        public void SendMessage(DummyMessage message)
        {
            Messages.Add(message);
        }
        public int GetMessageCount()
        {
            return Messages.Count;
        }
    }
}
