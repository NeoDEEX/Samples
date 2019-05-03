using System;
using System.ServiceModel;

namespace InterfaceLib
{
    [ServiceContract]
    public interface IWcfService4 : IDisposable
    {
        [OperationContract]
        string EchoUserInfo();
    }
}
