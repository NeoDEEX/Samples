using NeoDEEX.ServiceModel;
using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Client.Biz;
using Spectre.Console;

namespace client_app;

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

    // Log ID 를 구하는 간단한 예제
    static void LogIdSample()
    {
        AnsiConsole.MarkupLine("[green]Log ID sample...[/]");
        FoxBizResponse response = ExecuteSingle(FoxBizRequestDiagnostics.LogId);
        if ((response.Diagnostics & FoxBizResponseDiagnostics.HasLogId) == FoxBizResponseDiagnostics.HasLogId)
        {
            AnsiConsole.MarkupLine($"[blue]Response has log ID:[/] [yellow]{response.LogId}[/]");
        }
    }

    // ExecuteMultiple 를 사용한 경우 Log ID 를 구하는 예제
    static void LogIdInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]Log ID in ExecuteMultiple sample...[/]");
        FoxBizResponseCollection responses = ExecuteMultiple(FoxBizRequestDiagnostics.LogId);
        if ((responses.Diagnostics & FoxBizResponseDiagnostics.HasLogId) == FoxBizResponseDiagnostics.HasLogId)
        {
            AnsiConsole.MarkupLine($"[blue]ResponseCollection has log ID:[/] [yellow]{responses.LogId}[/]");
        }
        // 개별 Response 에는 LogId 가 포함되지 않는다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxBizResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].LogId: {response.LogId ?? "(null)"}[/]");
        }
    }

    // 요청 시 로깅을 통해 서비스 로그를 반환 받는 예제
    static void OnDemandLoggingSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Logging sample...[/]");
        FoxBizResponse response = ExecuteSingle(FoxBizRequestDiagnostics.ServiceLog);
        if ((response.Diagnostics & FoxBizResponseDiagnostics.HasServiceLog) == FoxBizResponseDiagnostics.HasServiceLog)
        {
            AnsiConsole.MarkupLine("[yellow]Response has service log.[/]");
            foreach (string logItem in response.ServiceLog)
            {
                AnsiConsole.MarkupLine($"[yellow]>[/] [gray]{Markup.Escape(logItem)}[/]");
            }
        }
    }

    // ExecuteMultiple 를 사용한 경우 요청 시 로깅을 통해 서비스 로그를 반환 받는 예제
    static void OnDemandLoggingInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Logging in ExecuteMultiple sample...[/]");
        FoxBizResponseCollection responses = ExecuteMultiple(FoxBizRequestDiagnostics.ServiceLog);
        if ((responses.Diagnostics & FoxBizResponseDiagnostics.HasServiceLog) == FoxBizResponseDiagnostics.HasServiceLog)
        {
            AnsiConsole.MarkupLine("[yellow]ResponseCollection has service log.[/]");
            foreach (string logItem in responses.ServiceLog)
            {
                //AnsiConsole.MarkupLine($"[yellow]>[/] [gray]{Markup.Escape(logItem)}[/]");
                Console.WriteLine($"> {logItem}");
            }
        }
        // 개별 Response 에는 서비스 로그 아이템들이 포함되지 않는다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxBizResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].ServiceLog: {(response.ServiceLog == null ? "(null)" : responses.ServiceLog.Length.ToString())}[/]");
        }
    }

    // 쿼리 수행 시간을 표시하는 예제
    static void ElapsedTimeSample()
    {
        AnsiConsole.MarkupLine("[green]Elapsed Time sample...[/]");
        FoxBizResponse response = ExecuteSingle();
        AnsiConsole.MarkupLine($"[blue]Response has elapsed time:[/] [yellow]{response.ElapsedMilliseconds} msec[/]");
    }

    // ExecuteMultiple 를 사용한 경우 쿼리 수행 시간을 표시하는 예제
    static void ElapsedTimeInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]Elapsed Time in ExecuteMultiple sample...[/]");
        FoxBizResponseCollection responses = ExecuteMultiple();
        AnsiConsole.MarkupLine($"[blue]ResponseCollection has elapsed time:[/] [yellow]{responses.ElapsedMilliseconds} msec[/]");
        // 다른 진단 플래그와 달리 개별 Response 에도 개별 쿼리 수행 시간이 반환된다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxBizResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].ElapsedMilliseconds: {response.ElapsedMilliseconds}[/]");
        }
    }

    // 요청 시 성능 정보를 반환 받는 예제
    static void OnDemandPerformanceLogSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Performance Log sample...[/]");
        FoxBizResponse response = ExecuteSingle(FoxBizRequestDiagnostics.PerformanceInfo);
        if ((response.Diagnostics & FoxBizResponseDiagnostics.HasPerformanceInfo) == FoxBizResponseDiagnostics.HasPerformanceInfo)
        {
            AnsiConsole.MarkupLine("[yellow]Response has performance log.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(response.PerformanceInfo.ToString())}[/]");
        }
    }

    // ExecuteMultiple 를 사용한 경우 요청 시 성능 정보를 반환 받는 예제
    static void OnDemandPerformanceLogInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Performance Log in ExecuteMultiple sample...[/]");
        FoxBizResponseCollection responses = ExecuteMultiple(FoxBizRequestDiagnostics.PerformanceInfo);
        if ((responses.Diagnostics & FoxBizResponseDiagnostics.HasPerformanceInfo) == FoxBizResponseDiagnostics.HasPerformanceInfo)
        {
            AnsiConsole.MarkupLine("[yellow]ResponseCollection has performance log.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(responses.PerformanceInfo.ToString())}[/]");
        }
        // 개별 Response 에는 성능 정보가 포함되지 않는다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxBizResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].PerformanceInfo: {(response.PerformanceInfo == null ? "(null)" : response.PerformanceInfo.ToString())}[/]");
        }
    }

    // Factory 예제
    static void FactorySimpleSample()
    {
        AnsiConsole.MarkupLine("[green]Factory Simple sample...[/]");
        FoxBizServiceClient client = new("/api/bizservice");
        SimpleBizRequestFactory factory = new(UseDiagnosticsMode);
        FoxBizRequestFactory saved = FoxBizRequest.Factory;
        FoxBizRequest.Factory = factory;
        try
        {
            FoxBizResponse response = client.Execute(factory.Create("BizLogic", "HelloWorld"));
            AnsiConsole.MarkupLine("[blue]Response.Diagnostics:[/] " + response.Diagnostics);
            if ((response.Diagnostics & FoxBizResponseDiagnostics.HasServiceLog) == FoxBizResponseDiagnostics.HasServiceLog)
            {
                AnsiConsole.MarkupLine("[yellow]Response has service log.[/]");
                foreach (string logItem in response.ServiceLog)
                {
                    AnsiConsole.MarkupLine($"[yellow]>[/] [gray]{Markup.Escape(logItem)}[/]");
                }
            }
        }
        finally
        {
            // 다른 테스트를 위해 FoxBizRequest.Factory 를 원래대로 복원한다.
            FoxBizRequest.Factory = saved;
        }
    }

    // 단일 요청을 수행하는 공통 메서드
    static FoxBizResponse ExecuteSingle(FoxBizRequestDiagnostics? diagnostics = null, string? classId = "BizLogic", string? methodId = "Echo")
    {
        FoxBizServiceClient client = new("api/bizservice");
        FoxBizRequest request = new(classId, methodId);
        request["input"] = "Hello World!";
        if (diagnostics != null)
        {
            request.Diagnostics = diagnostics.Value;
        }
        request.SerializeClientInfo = false;
        FoxBizResponse response = client.Execute(request);
        AnsiConsole.MarkupLine("[blue]Response.Diagnostics:[/] " + response.Diagnostics);
        return response;
    }

    // 다중 요청을 수행하는 공통 메서드
    static FoxBizResponseCollection ExecuteMultiple(FoxBizRequestDiagnostics? diagnostics = null)
    {
        FoxBizServiceClient client = new("/api/bizservice");
        FoxBizRequest request1 = new("BizLogic", "Echo");
        FoxBizRequest request2 = new("BizLogic", "HelloWorld");
        request1["input"] = "Hello World!";
        FoxBizRequestCollection requests = [request1, request2];
        if (diagnostics != null)
        {
            requests.Diagnostics = diagnostics.Value;
        }
        FoxBizResponseCollection responses = client.ExecuteMultiple(requests);
        AnsiConsole.MarkupLine("[blue]ResponseCollection.Diagnostics:[/] " + responses.Diagnostics);
        return responses;
    }
}
