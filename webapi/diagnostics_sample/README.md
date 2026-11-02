# Fox Biz/Data Service Diagnostics Sample

이 코드는 Fox Biz/Data Service 의 진단 기능을 활용하는 간단한 예제 코드를 보여 줍니다.

Fox Data Service 는 사용자 코드가 관여하지 않기 때문에 다양한 진단 기능을 제공하여 쿼리 수행 과정에서 발생하는 문제를 쉽게 파악할 수 있도록 도와줍니다. Fox Data Service 의 진단 기능들에 대한 상세한 내용은 [Fox Data Service 고급 사용법](https://doc.neodeex.net/webapi/dataservice/adv_usage/) 문서에서 진단 관련 항목을 참고 하십시요.

Fox Biz Service 는 비즈니스 로직 메서드를 디버깅 할 수 있기 때문에 상대적으로 문제점 파악이 용이 합니다. 하지만 Fox Biz Service 가 제공하는 다양한 진단 및 성능 측정 기능은 비즈니스 로직을 개발하고 운영, 유지 보수하는데 많은 도움을 줄 것입니다. Fox Biz Service 의 진단 기능들에 대한 상세한 내용은 [Fox Biz Service 고급 사용법](https://doc.neodeex.net/webapi/dataservice/adv_usage/) 문서에서 진단 관련 항목을 참고 하십시요.

이 코드는 다수의 예를 포함하고 있기 때문에 많은 출력이 발생합니다. 따라서 DataServiceTest 클래스 혹은 BizServiceTest 클래스에서 테스트하고자 하는 예제 메서드만을 수행시키는 것을 권장합니다. 예를 들어 요청 시 로깅 예제를 테스트 하고자 한다면 `OnDemandLoggingSample` 메서드의 주석을 제거하고 실행하면 됩니다.

```cs
internal static class DataServiceTest
{
    static readonly bool UseDiagnosticsMode = true;

    public static void DoTest()
    {
        AnsiConsole.MarkupLine("[cyan]> Data Service Test =======================================[/]");
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
    ......
}
```

```cs
internal static class BizServiceTest
{
    static readonly bool UseDiagnosticsMode = true;

    public static void DoTest()
    {
        AnsiConsole.MarkupLine("[cyan]> Biz Service Test ========================================[/]");
        //LogIdSample();
        //LogIdInExecuteMultipleSample();
        OnDemandLoggingSample();
        //OnDemandLoggingInExecuteMultipleSample();
        //ElapsedTimeSample();
        //ElapsedTimeInExecuteMultipleSample();
        //OnDemandPerformanceLogSample();
        //OnDemandPerformanceLogInExecuteMultipleSample();

        // Fox Biz Service 는 DB Profile 기능은 제공하지 않음

        //FactorySimpleSample();
    }
    ......
}
```

---
