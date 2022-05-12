# 타임아웃 서비스 구현 예제

이 예제는 일정 시간 이상 수행되어서는 안되는 트랜젝셔널 비즈 서비스를 구현하는 방법을 보여줍니다.

예를 들어, 일정 데이터를 배치 성으로 처리하는데 사용자가 너무 많은 데이터를 처리하도록 요청하는 경우, 이를 막기 위해 타임아웃을 지정해야 하는 상황을 생각해 볼 수 있습니다.

일반적으로 어떠한 작업을 수행하고 타임아웃을 지정하기 위해서는 필연적으로 별도의 스레드를 구동하고 해당 스레드의 종료를 감시하면서 타임아웃을 지정하는 것입니다. 그런데 이 작업이 트랜잭션이 적용되어야 한다면 구현이 복잡해 질 수 있습니다. NeoDEEX가 제공하는 트랜잭션 컴포넌트는 단일 스레드에서만 트랜잭션이 작동하기 때문입니다. 이러한 제약은 NeoDEEX 뿐만 아니라 대부분의 트랜잭션 프레임워크(System.Transactions 포함)에서도 동일합니다.

따라서 배치 작업를 트랜잭션 컴포넌트에서 시작하지 않고 POCO(Plain Old C# Object) 클래스에서 서비스 메서드를 구현하고 서비스 메서드에서 새로운 스레드를 만들고 이 스레드에서 트랜잭션 컴포넌트를 호출해야 합니다.

```cs
[FoxBizClass]
public class BatchFront
{
    [FoxBizMethod]
    public FoxBizResponse BatchLogic(FoxBizRequest request)
    {
        ...

        // 주어진 타임아웃 후 취소가 되는 토큰 생성
        var cts = new CancellationTokenSource(timeout);
        // 스레드 의존적 데이터 캡처
        var userInfo = FoxUserInfoContext.Current;
        // 트랜잭션이 수행되도록 별도의 스레드에서 Biz 컴포넌트를 호출한다.
        var task = Task.Run(() =>
        {
            // 새로운 스레드에서 수행될 수 있으므로 UserInfoContext를 설정해 주어야 한다.
            using (FoxUserInfoContext.CreateScope(userInfo))
            {
                using (var biz = new BizComp())
                {
                    biz.DoBatchJob(request, cts.Token);
                }
            }
        });
        try
        {
            task.Wait();
        }
        catch (AggregateException ex)
        {
            WriteLog(TheOne.Diagnostics.FoxLogLevel.Error, ex.Flatten().InnerExceptions[0].Message);
            response.Success = false;
        }
        return response;
    }
}
```

한편, 배치를 실질적으로 수행하는 트랜잭션 컴포넌트는 매개변수로 전달받은 `CancellationToken` 객체를 사용하여 취소 여부를 확인하도록 합니다. 타임아웃에 의해 취소가 되었다면 예외를 발생(`ThrowIfCancellationRequested` 메서드)하여 작업이 중단되고 트랜잭션이 롤백되게 됩니다.

```cs
[FoxTransaction(TransactionOption = FoxTransactionOption.Required]
public class BizComp : FoxBizBase
{
    public void DoBatchJob(FoxBizRequest request, CancellationToken ct)
    {
        ......
        for(int i = 0; i < iteration; i++)
        {
            using (var dac = new DacComp())
            {
                dac.DoDataJob();
            }
            // 취소 확인
            ct.ThrowIfCancellationRequested();
        }
        ......
    }
    ......
}
```

:information_source: 위 트랜잭션 컴포넌트의 `DoBatchJob` 메서드는 `FoxBizMethod` 특성이 명시되어 있지 않음에 주목하십시요. 클라이언트 이 메서드를 직접 호출하지 못하게 하기 위함입니다.

---
