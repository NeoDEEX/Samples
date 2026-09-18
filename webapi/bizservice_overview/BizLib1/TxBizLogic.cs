using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;
using System.Data;

namespace BizLib1;

#pragma warning disable CA1822 // Mark members as static

[FoxBizClass]
public class TxBizLogic : FoxBizBase
{
    [FoxBizMethod]
    public void InsertTxData(string data)
    {
        using TxDataAccess dac = new();
        ITxDataAccess itf = dac.CreateExecution<ITxDataAccess>();
        itf.InsertTestData(data);
    }

    [FoxBizMethod]
    [FoxTransaction(FoxTransactionOption.Suppress)]
    public DataSet GetTestData()
    {
        using TxDataAccess dac = new();
        ITxDataAccess itf = dac.CreateExecution<ITxDataAccess>();
        var ds = itf.GetTestData();
        return ds;
    }

    [FoxBizMethod]
    public void InitTestData()
    {
        using TxDataAccess dac = new();
        ITxDataAccess itf = dac.CreateExecution<ITxDataAccess>();
        itf.InitTestData();
    }
}
