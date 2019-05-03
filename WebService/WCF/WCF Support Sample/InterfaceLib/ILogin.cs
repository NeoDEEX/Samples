using System;
using System.ServiceModel;

namespace InterfaceLib
{
    [ServiceContract]
    public interface ILogin : IDisposable
    {
        [OperationContract]
        bool LoginUser(string userId, string password);
    }
}
