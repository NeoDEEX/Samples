using BizLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceWeb
{
    //
    // WCF 서비스 클래스 구현
    //
    public class OldWcfService : IOldWcfService
    {
        public DataSet GetAllProducts()
        {
            // 비즈니스 로직 클래스를 호출한다.
            using (var biz = new BizLogicClass())
            {
                return biz.GetAllProducts();
            }
        }
    }
}
