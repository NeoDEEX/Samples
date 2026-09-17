# Fox Data Service Diagnostics Sample

이 코드는 Fox Data Service 의 진단 기능을 활용하는 간단한 예제 코드를 보여 줍니다.

Fox Data Service 는 사용자 코드가 관여하지 않기 때문에 다양한 진단 기능을 제공하여 쿼리 수행 과정에서 발생하는 문제를 쉽게 파악할 수 있도록 도와줍니다. Fox Data Service 의 진단 기능들에 대한 상세한 내용은 [Fox Data Service 고급 사용법](https://doc.neodeex.net/webapi/dataservice/adv_usage/) 문서에서 진단 관련 항목을 참고 하십시요.

이 코드는 다수의 예를 포함하고 있기 때문에 많은 출력이 발생합니다. 따라서 `Main` 메서드에서 테스트 하고자 하는 샘플 메서드의 주석을 제거하고 나머지 메서드의 주석을 추가하여 실행하는 것을 권장합니다. 예를 들어 요청 시 로깅 예제를 테스트 하고자 한다면 `OnDemandLoggingSample` 메서드의 주석을 제거하고 실행하면 됩니다.

```cs
static void Main()
{
    AnsiConsole.MarkupLine("[green]Fox Biz/Data Service Diagnostics Sample Client...[/]");
    AnsiConsole.WriteLine();

    //LogIdSample();
    //LogIdInExecuteMultipleSample();
    OnDemandLoggingSample();
    //OnDemandLoggingInExecuteMultipleSample();
    //ElapsedTimeSample();
    //ElapsedTimeInExecuteMultipleSample();
    //OnDemandPerformanceLogSample();
    //OnDemandPerformanceLogInExecuteMultipleSample();
    //OnDemandDbProfileInfoSample();
    //OnDemandDbProfileInfoInExecuteMultipleSample();
    //SupressPerformanceLogWriteSample();
    //FactorySimpleSample();
}
```

---
