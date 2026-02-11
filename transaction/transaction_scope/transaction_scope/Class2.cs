using NeoDEEX.Transactions;

namespace transaction_scope;

public class Class2 : TestBaseClass, IClass2
{
    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    [FoxAutoComplete]
    public void TxMethod_A2(int indent)
    {
        DumpInfo(indent++);

        using IClass3 itf = CreateTxObject<Class3, IClass3>();
        itf.TxMethod_A3(indent);
        itf.TxMethod_A4(indent);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Required)]
    [FoxAutoComplete]
    public void TxMethod_B2(int indent)
    {
        DumpInfo(indent++);

        using IClass3 itf = CreateTxObject<Class3, IClass3>();
        itf.TxMethod_B3(indent);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Required)]
    [FoxAutoComplete]
    public void TxMethod_C2(int indent)
    {
        DumpInfo(indent++);

        using IClass3 itf = CreateTxObject<Class3, IClass3>();
        itf.TxMethod_C3(indent);
        itf.TxMethod_D3(indent);
    }
}

public interface IClass2 : IDisposable
{
    void TxMethod_A2(int indent);
    void TxMethod_B2(int indent);
    void TxMethod_C2(int indent);
}
