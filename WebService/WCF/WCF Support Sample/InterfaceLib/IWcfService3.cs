using System;
using System.ServiceModel;

namespace InterfaceLib
{
    [ServiceContract]
    public interface IWcfService3 : IDisposable
    {
        [OperationContract]
        void DoWork();
    }
}
