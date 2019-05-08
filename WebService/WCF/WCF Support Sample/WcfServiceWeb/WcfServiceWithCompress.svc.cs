using BizLib;
using InterfaceLib;
using System.Data;

namespace WcfServiceWeb
{
    //
    // 서비스 클래스 구현
    //
    public class WcfServiceWithCompress : IWcfServiceWithCompress
    {
        public DataSet GetAllProducts()
        {
            // 비즈니스 로직을 호출한다.
            using (var comp = new BizLogicClass())
            {
                return comp.GetAllProducts();
            }
        }

        public void Dispose()
        {
            // 구현 내용이 없다.
        }
    }
}
