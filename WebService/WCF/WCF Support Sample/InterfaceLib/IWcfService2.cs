using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InterfaceLib
{
    //
    // 데모를 위한 두번째 WCF 서비스 인터페이스
    //
    [ServiceContract]
    public interface IWcfService2 : IDisposable
    {
        [OperationContract]
        void DoWork();
    }
}
