using System;
using System.Collections.Generic;
namespace DummyServer
{
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using Contracts;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void SendMessage(DummyMessage message);
        int GetMessageCount();
    }
}
