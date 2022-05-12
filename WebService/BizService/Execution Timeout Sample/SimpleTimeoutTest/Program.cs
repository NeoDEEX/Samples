using System;
using System.Data;
using System.Threading.Tasks;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;

namespace SimpleTimeoutTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 테스트 데이터 클리어.
            //await ClearTestData();

            // 배치 수행 후 데이터 건수 표시
            var ds = await GetTestData();
            Console.WriteLine($"Before batch: data count = {ds.Tables[0].Rows.Count}");

            // 배치 수행
            await RunBatch(3000);

            // 배치 수행 후 데이터 건수 표시
            ds = await GetTestData();
            Console.WriteLine($"After batch: data count = {ds.Tables[0].Rows.Count}");
            //Dump(ds);
        }

        // 테스트 데이터를 덤프 한다.
        static void Dump(DataSet ds)
        {
            var dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("  No Data");
            }
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                int id = row.Field<int>("Id");
                string desc = row.Field<string>("Desc");
                DateTime timestamp = row.Field<DateTime>("CreateTime");
                Console.WriteLine($"  {id,-4} {timestamp,-12:T} {desc}");
            }
        }

        // 테스트 데이터를 조회한다.
        static async Task<DataSet> GetTestData()
        {
            Console.WriteLine("Reading test data...");
            using (var client = new FoxRestBizClient("", ""))
            {
                var request = new FoxBizRequest("ServiceWebApp.Biz.BizComp", "GetTestData");
                var response = await client.ExecuteAsync(request);
                return response.DataSet;
            }
        }

        // 테스트용 데이터를 클리어 한다.
        static async Task ClearTestData()
        {
            Console.WriteLine("Clearing test data...");
            using (var client = new FoxRestBizClient("", ""))
            {
                var request = new FoxBizRequest("ServiceWebApp.Biz.BizComp", "ClearTestData");
                await client.ExecuteAsync(request);
            }
        }

        // 배치 서비스를 호출한다.
        static async Task<FoxBizResponse> RunBatch(int timeout)
        {
            Console.WriteLine("Executing bath...");
            Task<FoxBizResponse> invokeTask;
            //FoxBizResponse response;
            using (var client = new FoxRestBizClient("", ""))
            {
                var request = new FoxBizRequest("ServiceWebApp.Biz.BatchFront", "BatchLogic");
                // 타임아웃 시간을 매개변수로 전달한다.
                request["timeout"] = timeout;
                invokeTask = client.ExecuteAsync(request);
                // 배치가 수행되는 동안 진행 상황(progress)을 표시하는 task
                Task waitTask = Task.Run(async () =>
                {
                    int waitingTime = 0;
                    int incr = 100;
                    while (invokeTask.IsCompleted == false)
                    {
                        Console.Write($"  Waiting {waitingTime / 1000.0:N2} sec.\r");
                        await Task.Delay(incr);
                        waitingTime += incr;
                    }
                });
                // 배치 완료를 기다린다.
                await Task.WhenAny(invokeTask, waitTask);
            }
            Console.WriteLine();
            var response = invokeTask.Result;
            Console.WriteLine($"Batch execution time: {response.ElapsedMilliseconds:N3} msec");
            if (response.Success == true)
            {
                Console.WriteLine("Batch end successfully... Transaction COMMITTED.");
            }
            else
            {
                Console.WriteLine("Batch stopped by timeout... Transaction ROLLBACKED.");
            }
            return response;
        }
    }
}
