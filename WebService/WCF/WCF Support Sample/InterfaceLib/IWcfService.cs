using System;
using System.Data;
using System.ServiceModel;

namespace InterfaceLib
{
    //
    // 서비스 인터페이스 선언. IDisposable 에서 파생해야 한다.
    // (웹 프로젝트에서 복사해 왔다면 네임스페이스 변경에 주의해야 한다)
    //
    [ServiceContract]
    public interface IWcfService : IDisposable
    {
        [OperationContract]
        DataSet GetAllProducts();
    }
}
