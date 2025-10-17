using NeoDEEX.Transactions;

namespace TransactionalComponent;

public interface IBizComp
{
    void InsertData(params int[] ids);
}

public class BizComp : FoxBizBase, IBizComp
{
    public void InsertData(params int[] ids)
    {
        using DacComp dac = new();
        IDacComp itf = dac.CreateExecution<IDacComp>();
        foreach (int id in ids)
        {
            itf.InsertData(id);
        }
    }
}
