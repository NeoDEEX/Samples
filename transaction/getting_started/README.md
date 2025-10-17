# Transactional Component Example

Fox Transactions 을 사용하여 트랜잭션을 처리하는 컴포넌트(클래스) 예제입니다. 다음 `InsertData` 메서드는 매개변수로 주어진 데이터를 모두 추가하거나(commit 상황), 오류(예외)가 발생하면 하나도 추가가되지 않습니다(rollback 상황).

```cs
public interface IBizComp
{
    void InsertData(params int[] ids);
}

// Transational component
public class BizComp : FoxBizBase, IBizComp
{
    // 여러 데이터를 트랜잭션 하에서 INSERT 하는 메서드
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

static void Main()
{
    using BizComp biz = new();
    IBizComp itf = biz.CreateExecution<IBizComp>();
    itf.InsertData(990, 991, 991);
}     
```

이 예제에 대한 상세한 정보는 [Fox Transactions: Getting Started 문서](https://neodeex.github.io/doc/transaction/getting_started/)를 참고 하십시요.

---
