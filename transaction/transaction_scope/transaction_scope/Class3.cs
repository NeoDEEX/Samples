using NeoDEEX.Transactions;

namespace transaction_scope;

public class Class3 : TestBaseClass, IClass3
{
    [FoxTransaction(TransactionOption = FoxTransactionOption.Suppress)]
    [FoxAutoComplete]
    public void TxMethod_A3(int indent)
    {
        DumpInfo(indent++);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    [FoxAutoComplete]
    public void TxMethod_A4(int indent)
    {
        DumpInfo(indent++);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    [FoxAutoComplete]
    public void TxMethod_B3(int indent)
    {
        DumpInfo(indent++);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.Supported)]
    [FoxAutoComplete]
    public void TxMethod_C3(int indent)
    {
        DumpInfo(indent++);
    }

    [FoxTransaction(TransactionOption = FoxTransactionOption.RequiresNew)]
    [FoxAutoComplete]
    public void TxMethod_D3(int indent)
    {
        DumpInfo(indent++);
    }
}

public interface IClass3 : IDisposable
{
    void TxMethod_A3(int indent);
    void TxMethod_A4(int indent);
    void TxMethod_B3(int indent);
    void TxMethod_C3(int indent);
    void TxMethod_D3(int indent);
}
