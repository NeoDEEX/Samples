# ExecuteMultiple 마이그레이션 예제

레거시 코드 중 서비스를 여러 차례 호출하는 코드를 ExecuteMultiple 메서드를 사용하여 마이그레이션하는 예제 입니다.

```cs
// 레거시 코드
var oService = FoxClientFactory.CreateChannel<ILegacyService>(...);
using (oService as IDisposable)
{
    this.dsDataField1 = oService.GetData(1, 2, 3, 4);
    this.dsDataField2 = oService.SomeData("param1", "param2");
}
```

이 예제는 [ExecuteCollection](https://github.com/NeoDEEX/Samples/blob/f83dda480be96a834909902fa7609cc4834aff1f/WebService/BizService/Migration%20Examples/ExecuteMultipleMigration/ExecutionCollection.cs#L11) 클래스를 작성하여 기존 호출들에 대한 정보를 수집하고 [MyBizClient](https://github.com/NeoDEEX/Samples/blob/f83dda480be96a834909902fa7609cc4834aff1f/WebService/BizService/Migration%20Examples/ExecuteMultipleMigration/MyBizClient.cs#L11) 클래스에서 ExecuteMultiple 메서드를 호출한 후 그 결과를 처리합니다. ExecutionCollection 클래스와 MyBizClient 클래스의 상세한 구현 방법은 예제 코드를 참고 하십시요.

```cs
using (var client = new MyBizClient("your_class_id"))
{
    var executions = new ExecutionCollection();
    executions.AddExecute("GetData", ds => this.dsDataField1 = ds, 1, 2, 3, 4);
    executions.AddExecute("SomeData", ds => this.dsDataField2 = ds, "param1", "param2");
    client.ExecuteMultiple(executions);
}
```
