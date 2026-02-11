using NeoDEEX.Transactions;

namespace transaction_scope;

public class Class1 : TestBaseClass, IClass1
{
    [FoxTransaction(TransactionOption = FoxTransactionOption.Required)]
    [FoxAutoComplete]
    public void TxMethod_A1(int indent)
    {
        DumpInfo(indent++);

        using IClass2 itf = CreateTxObject<Class2, IClass2>();
        itf.TxMethod_A2(indent);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.RequiresNew)]
    [FoxAutoComplete]
    public void TxMethod_B1(int indent)
    {
        DumpInfo(indent++);

        using IClass2 itf = CreateTxObject<Class2, IClass2>();
        itf.TxMethod_B2(indent);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    [FoxAutoComplete]
    public void TxMethod_C1(int indent)
    {
        DumpInfo(indent++);

        using IClass2 itf = CreateTxObject<Class2, IClass2>();
        itf.TxMethod_C2(indent);
    }
}

public interface IClass1 : IDisposable
{
    void TxMethod_A1(int indent);
    void TxMethod_B1(int indent);
    void TxMethod_C1(int indent);
}
