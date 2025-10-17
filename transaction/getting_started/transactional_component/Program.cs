//
// NeoDEEX 5.x 예제 코드
//
// Transactional Component 예제
//

namespace TransactionalComponent;

internal class Program
{
    static void Main()
    {
        TxCompTest();
        BizCompTest();
    }

    static void BizCompTest()
    {
        using BizComp biz = new();
        IBizComp itf = biz.CreateExecution<IBizComp>();
        itf.InsertData(990, 991, 992);
        //biz.InsertData(990, 991, 992);
    }

    static void TxCompTest()
    {
        using TxComp comp = new();
        ITxComp itf = comp.CreateExecution<ITxComp>();
        try
        {
            itf.InsertData(992);
            //comp.InsertData(992);
        }
        catch (InvalidOperationException)
        {
            // 예외 무시
        }
    }
}
