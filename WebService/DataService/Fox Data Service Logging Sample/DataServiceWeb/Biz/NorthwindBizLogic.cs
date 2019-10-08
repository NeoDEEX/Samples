using CommonLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheOne.Data;
using TheOne.ServiceModel.Biz;
using TheOne.Transactions;

namespace DataServiceWeb.Biz
{
    //
    // 비즈 서비스 성능 로그를 보여주기 위한 예제 비즈 로직 클래스
    //
    [FoxBizClass]
    public class NorthwindBiz : FoxBizBase
    {
        [FoxBizMethod]
        public DataSet GetBaseData()
        {
            var ds = new DataSet();
            // 다양한 메서드 호출의 예를 보이기 위해 DAC을 2회 호출하고
            // 그 결과를 하나의 DataSet으로 구성하였다.
            using (var dac = new NorthwindDac())
            {
                var ds1 = dac.GetCategories();
                var dt1 = ds1.Tables[0];
                dt1.TableName = "Categories";
                ds1.Tables.Remove(dt1);
                ds.Tables.Add(dt1);

                var ds2 = dac.GetSuppliers();
                var dt2 = ds2.Tables[0];
                dt2.TableName = "Suppliers";
                ds2.Tables.Remove(dt2);
                ds.Tables.Add(dt2);
            }
            return ds;
        }

        [FoxBizMethod]
        public DataSet GetProducts(int? categoryId, int? supplierId)
        {
            using (var dac = new NorthwindDac())
            {
                return dac.GetProducts(categoryId, supplierId);
            }
        }

        [FoxBizMethod]
        public DataSet GenerateError()
        {
            using (var dac = new NorthwindDac())
            {
                return dac.GenerateError();
            }
        }
    }

    // 예제 용 DAC 컴포넌트
    // 이 예제에서 클라이언트가 직접 DAC을 호출하지 않기 때문에 FoxBizClass/FoxBizMethod를 제공하지 않았다.
    public class NorthwindDac : FoxDacBase
    {
        public DataSet GetCategories()
        {
            return this.DbAccess.ExecuteQueryDataSet("Northwind.GetCategories");
        }

        public DataSet GetSuppliers()
        {
            return this.DbAccess.ExecuteQueryDataSet("Northwind.GetSuppliers");
        }

        public DataSet GetProducts(int? categoryId, int? supplierId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "CategoryID", categoryId },
                { "SupplierID", supplierId }
            };
            parameters.SetNullToDBNull();
            return this.DbAccess.ExecuteQueryDataSet("Northwind.GetProducts", parameters);
        }

        public DataSet GenerateError()
        {
            return this.DbAccess.ExecuteQueryDataSet("Northwind.ErrorQuery");
        }
    }
}