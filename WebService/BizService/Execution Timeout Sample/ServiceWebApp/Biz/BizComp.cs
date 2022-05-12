using System.Data;
using System.Threading;
using TheOne.Diagnostics;
using TheOne.ServiceModel.Biz;
using TheOne.Transactions;

namespace ServiceWebApp.Biz
{
    [FoxBizClass]
    [FoxTransaction(TransactionOption = FoxTransactionOption.Required, Timeout = 30)]
    public class BizComp : FoxBizBase
    {
        // 이 메서드는 BatchFront를 통해서만 호출하도록 FoxBizMethod 특성을 사용하지 말아야 한다.
        // 주) FoxBizServiceContext 클래스는 최신 버전의 NeoDEEX에서 제공되는 기능이므로
        //     하위 버전을 사용하는 경우 이 매개변수를 제거해야 한다.
        public void DoBatchJob(FoxBizRequest request, CancellationToken ct, FoxBizServiceContext bizContext)
        {
            int iteration = 6;
            for(int i = 0; i < iteration; i++)
            {
                using (var dac = new DacComp())
                {
                    dac.DoDataJob();
                }
                // 주) FoxBizServiceContext 클래스를 지원하지 않는 버전을 사용 중이라면 GetLogger를 호출하여
                ///    로그를 기록하도록 한다.
                bizContext?.WriteLog(FoxLogLevel.Information, "Insert #{0}", i + 1);
                // 예외를 유발하여 트랜잭션이 롤백되도록 한다.
                ct.ThrowIfCancellationRequested();
                // 테스트를 위한 지연 수행
                System.Threading.Thread.Sleep(1000);
            }
        }

        [FoxBizMethod]
        public DataSet GetTestData()
        {
            using (var dac = new DacComp())
            {
                return dac.GetTestData();
            }
        }

        [FoxBizMethod]
        public void ClearTestData()
        {
            using (var dac = new DacComp())
            {
                dac.ClearTestData();
            }
        }
    }
}