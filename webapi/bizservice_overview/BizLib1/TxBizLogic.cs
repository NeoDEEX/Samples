using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;
using System.Data;

namespace BizLib1;

#pragma warning disable CA1822 // Mark members as static

[FoxTransaction(FoxTransactionOption.Required)]
[FoxBizClass]
public class TxBizLogic : FoxBizBase
{
    [FoxBizMethod]
    public void InsertTxData(int pk, string data)
    {
        using TxDataAccess dac = new();
        if (pk < 0 || pk > 9)
        {
            throw new ArgumentOutOfRangeException(nameof(pk), "pk must be between 0 and 9.");
        }
        ITxDataAccess itf = dac.CreateExecution<ITxDataAccess>();
        itf.InsertTestData(pk, data);
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
