# Fox Transactions 선언적 트랜잭션 예제

[Fox Transactions](https://neodeex.github.io/doc/transaction/) 에서 제공하는 선언적 트랜잭션 기능을 보여주는 예제 입니다.

[선언적 트랜잭션](https://neodeex.github.io/doc/transaction/declarative_transaction/)은 `FoxBizBase`, `FoxDacBase` 와 같은 베이스 클래스와 더불어 `FoxTransactionAttribute`, `FoxAutoCompleteAttribute` 특성을 사용하여 트랜잭션의 시작/커밋/롤백을 자동으로 처리할 수 있습니다.

이 예제는 간단한 Web API 예제를 통해 선언적으로 트랜잭션을 처리하는 예를 보여줍니다.

```cs
[FoxTransaction(FoxTransactionOption.Required)]
public class MemoBiz : FoxBizBase, IMemoBiz
{
    // 트랜잭션은 자동으로 시작되고 오류(예외)가 발생하지 않으면
    // 메서드 종료와 더불어 트랜잭션은 커밋됩니다.
    [FoxAutoComplete]
    public List<int> InsertMany(Memo[] memos)
    {
        using MemoDac dac = new();
        IMemoDac itf = dac.CreateExecution<IMemoDac>();
        List<int> savedIds = [];
        foreach (var memo in memos)
        {
            int newId = itf.InsertMemo(memo);
            savedIds.Add(newId);
        }
        return savedIds;
    }
}
```

---
