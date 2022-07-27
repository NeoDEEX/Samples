using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.ServiceModel.Data;
using TheOne.ServiceModel.Web;

namespace SimpleMacro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LegacyQueryTest();
            //DynamicQueryTest();
            DataServiceTest();
        }
        
        static  void Dump(DataTable dt, string header = null)
        {
            if (header != null)
            {
                Console.Write(header + ": ");
            }
            Console.WriteLine($"Records = {dt.Rows.Count}");
            foreach(DataRow row in dt.Rows)
            {
                foreach(DataColumn column in dt.Columns)
                {
                    Console.Write("{0,-33}", row[column].ToString());
                }
                Console.WriteLine();
            }
        }

        static void LegacyQueryTest()
        {
            using (var dac = new LegacyDacComp())
            {
                var ds1 = dac.GetData(null);
                Dump(ds1.Tables[0]);
                var ds2 = dac.GetData(2);
                Dump(ds2.Tables[0]);
            }
        }

        static void DynamicQueryTest()
        {
            using (var dac = new DacComp())
            {
                var ds1 = dac.GetData(null);
                Dump(ds1.Tables[0]);
                var ds2 = dac.GetData(2);
                Dump(ds2.Tables[0]);
            }
        }

        static void DataServiceTest()
        {
            using (var client = new FoxRestDataClient())
            {
                client.ServiceUrl = "http://localhost:52570/api/dataservice";
                FoxDataRequest request = new FoxDataRequest("Query.GetData");
                request["CategoryId"] = 2;
                FoxDataResponse response = client.ExecuteDataSet(request);
                Dump(response.DataSet.Tables[0]);
            }
        }
    }
}
