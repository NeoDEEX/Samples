using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using Spectre.Console;

namespace client_app;

internal static class Program
{
    static bool UseDiagnosticsMode = false;

    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz/Data Service Diagnostics Sample Client...[/]");
        AnsiConsole.WriteLine();

        LogIdSample();
        //LogIdInExecuteMultipleSample();
        //OnDemandLoggingSample();
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

    // Log ID 를 구하는 간단한 예제
    static void LogIdSample()
    {
        AnsiConsole.MarkupLine("[green]Log ID sample...[/]");
        FoxDataResponse response = ExecuteSingle(FoxDataRequestDiagnostics.LogId);
        if ((response.Diagnostics & FoxDataResponseDiagnostics.HasLogId) == FoxDataResponseDiagnostics.HasLogId)
        {
            AnsiConsole.MarkupLine($"[blue]Response has log ID:[/] [yellow]{response.LogId}[/]");
        }
    }

    // ExecuteMultiple 를 사용한 경우 Log ID 를 구하는 예제
    static void LogIdInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]Log ID in ExecuteMultiple sample...[/]");
        FoxDataResponseCollection responses = ExecuteMultiple(FoxDataRequestDiagnostics.LogId);
        if ((responses.Diagnostics & FoxDataResponseDiagnostics.HasLogId) == FoxDataResponseDiagnostics.HasLogId)
        {
            AnsiConsole.MarkupLine($"[blue]ResponseCollection has log ID:[/] [yellow]{responses.LogId}[/]");
        }
        // 개별 Response 에는 LogId 가 포함되지 않는다.
        AnsiConsole.WriteLine();
        for(int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].LogId: {response.LogId ?? "(null)"}[/]");
        }
    }

    // 요청 시 로깅을 통해 서비스 로그를 반환 받는 예제
    static void OnDemandLoggingSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Logging sample...[/]");
        FoxDataResponse response = ExecuteSingle(FoxDataRequestDiagnostics.ServiceLog);
        if ((response.Diagnostics & FoxDataResponseDiagnostics.HasServiceLog) == FoxDataResponseDiagnostics.HasServiceLog)
        {
            AnsiConsole.MarkupLine("[yellow]Response has service log.[/]");
            foreach(string logItem in response.ServiceLog)
            {
                AnsiConsole.MarkupLine($"[yellow]>[/] [gray]{Markup.Escape(logItem)}[/]");
            }
        }
    }

    // ExecuteMultiple 를 사용한 경우 요청 시 로깅을 통해 서비스 로그를 반환 받는 예제
    static void OnDemandLoggingInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Logging in ExecuteMultiple sample...[/]");
        FoxDataResponseCollection responses = ExecuteMultiple(FoxDataRequestDiagnostics.ServiceLog);
        if ((responses.Diagnostics & FoxDataResponseDiagnostics.HasServiceLog) == FoxDataResponseDiagnostics.HasServiceLog)
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
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].ServiceLog: {(response.ServiceLog == null ? "(null)" : responses.ServiceLog.Length.ToString())}[/]");
        }
    }

    // 쿼리 수행 시간을 표시하는 예제
    static void ElapsedTimeSample()
    {
        AnsiConsole.MarkupLine("[green]Elapsed Time sample...[/]");
        FoxDataResponse response = ExecuteSingle();
        AnsiConsole.MarkupLine($"[blue]Response has elapsed time:[/] [yellow]{response.ElapsedMilliseconds} msec[/]");
    }

    // ExecuteMultiple 를 사용한 경우 쿼리 수행 시간을 표시하는 예제
    static void ElapsedTimeInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]Elapsed Time in ExecuteMultiple sample...[/]");
        FoxDataResponseCollection responses = ExecuteMultiple();
        AnsiConsole.MarkupLine($"[blue]ResponseCollection has elapsed time:[/] [yellow]{responses.ElapsedMilliseconds} msec[/]");
        // 다른 진단 플래그와 달리 개별 Response 에도 개별 쿼리 수행 시간이 반환된다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].ElapsedMilliseconds: {response.ElapsedMilliseconds}[/]");
        }
    }

    // 요청 시 성능 정보를 반환 받는 예제
    static void OnDemandPerformanceLogSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Performance Log sample...[/]");
        FoxDataResponse response = ExecuteSingle(FoxDataRequestDiagnostics.PerformanceInfo);
        if ((response.Diagnostics & FoxDataResponseDiagnostics.HasPerformanceInfo) == FoxDataResponseDiagnostics.HasPerformanceInfo)
        {
            AnsiConsole.MarkupLine("[yellow]Response has performance log.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(response.PerformanceInfo.ToString())}[/]");
        }
    }

    // ExecuteMultiple 를 사용한 경우 요청 시 성능 정보를 반환 받는 예제
    static void OnDemandPerformanceLogInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]On-Demand Performance Log in ExecuteMultiple sample...[/]");
        FoxDataResponseCollection responses = ExecuteMultiple(FoxDataRequestDiagnostics.PerformanceInfo);
        if ((responses.Diagnostics & FoxDataResponseDiagnostics.HasPerformanceInfo) == FoxDataResponseDiagnostics.HasPerformanceInfo)
        {
            AnsiConsole.MarkupLine("[yellow]ResponseCollection has performance log.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(responses.PerformanceInfo.ToString())}[/]");
        }
        // 개별 Response 에는 성능 정보가 포함되지 않는다.
        AnsiConsole.WriteLine();
        for (int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[gray]Response[[{i}]].PerformanceInfo: {(response.PerformanceInfo == null ? "(null)" : response.PerformanceInfo.ToString())}[/]");
        }
    }

    // 요청 시 DB 프로파일 정보를 반환 받는 예제
    static void OnDemandDbProfileInfoSample()
    {
        AnsiConsole.MarkupLine("[green]DB Profile Info sample...[/]");
        FoxDataResponse response = ExecuteSingle(FoxDataRequestDiagnostics.DbProfileInfo);
        if ((response.Diagnostics & FoxDataResponseDiagnostics.HasDbProfileInfo) == FoxDataResponseDiagnostics.HasDbProfileInfo)
        {
            AnsiConsole.MarkupLine("[yellow]Response has DB profile info.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(response.DbProfileInfo.ToString())}[/]");
        }
    }

    // ExecuteMultiple 를 사용한 경우 요청 시 DB 프로파일 정보를 반환 받는 예제
    static void OnDemandDbProfileInfoInExecuteMultipleSample()
    {
        AnsiConsole.MarkupLine("[green]DB Profile Info in ExecuteMultiple sample...[/]");
        FoxDataResponseCollection responses = ExecuteMultiple(FoxDataRequestDiagnostics.DbProfileInfo);
        // DB 프로파일 정보는 개별 Response 객체에만 포함된다.
        for (int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[yellow]Response[[{i}]].DbProfileInfo:[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(response.DbProfileInfo.ToString())}[/]");
        }
    }

    // DB 프로파일 정보 로깅을 억제하는 예제
    // 서버 측에서 DB 프로파일 정보가 로그에 기록되지 않지만, 요청 시 DB 프로파일 정보는 반환된다.
    static void SupressPerformanceLogWriteSample()
    {
        AnsiConsole.MarkupLine("[green]Supress Performance Log Write sample...[/]");
        FoxDataRequestDiagnostics diagnostics = FoxDataRequestDiagnostics.SuppressPerfLogWrite | FoxDataRequestDiagnostics.PerformanceInfo;
        // 상수를 반환하는 쿼리를 수행하고 서버 측 DB 프로파일 정보가 기록되는지 확인한다.
        FoxDataResponse response = ExecuteSingle(diagnostics, "sample.get_const");
        if ((response.Diagnostics & FoxDataResponseDiagnostics.HasPerformanceInfo) == FoxDataResponseDiagnostics.HasPerformanceInfo)
        {
            AnsiConsole.MarkupLine("[yellow]Response has Performance info.[/]");
            AnsiConsole.MarkupLine($"[gray]{Markup.Escape(response.PerformanceInfo.ToString())}[/]");
        }
    }

    static void FactorySimpleSample()
    {
        AnsiConsole.MarkupLine("[green]Factory Simple sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        SimpleRequestFactory factory = new(UseDiagnosticsMode);
        FoxDataRequestFactory saved = FoxDataRequest.Factory;
        FoxDataRequest.Factory = factory;
        try
        {
            FoxDataResponse response = client.ExecuteDataSet(factory.Create("sample.get_all_products"));
            AnsiConsole.MarkupLine("[blue]Response.Diagnostics:[/] " + response.Diagnostics);
            if ((response.Diagnostics & FoxDataResponseDiagnostics.HasServiceLog) == FoxDataResponseDiagnostics.HasServiceLog)
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
            // 다른 테스트를 위해 FoxDataRequest.Factory 를 원래대로 복원한다.
            FoxDataRequest.Factory = saved;
        }
    }

    // 단일 요청을 수행하는 공통 메서드
    static FoxDataResponse ExecuteSingle(FoxDataRequestDiagnostics? diagnostics = null, string? queryId = "sample.get_all_products")
    {
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequest request = new(queryId);
        if (diagnostics != null)
        {
            request.Diagnostics = diagnostics.Value;
        }
        request.SerializeClientInfo = false;
        FoxDataResponse response = client.ExecuteDataSet(request);
        AnsiConsole.MarkupLine("[blue]Response.Diagnostics:[/] " + response.Diagnostics);
        return response;
    }

    static FoxDataResponseCollection ExecuteMultiple(FoxDataRequestDiagnostics? diagnostics = null)
    {
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequest request1 = new("sample.get_all_products") { Operation = FoxDataOperations.ExecuteDataSet };
        FoxDataRequest request2 = new("sample.get_product_by_id") { Operation = FoxDataOperations.ExecuteDataSet };
        request2.Parameters.Add("product_id", "P001");
        FoxDataRequestCollection requests = [request1, request2];
        if (diagnostics != null)
        {
            if (diagnostics == FoxDataRequestDiagnostics.DbProfileInfo)
            {
                // DB 프로파일 정보는 개별 요청에만 포함되므로, ExecuteMultiple 호출 시에는 개별 요청에 설정해야 한다.
                request1.Diagnostics = diagnostics.Value;
                request2.Diagnostics = diagnostics.Value;
            }
            else
            {
                requests.Diagnostics = diagnostics.Value;
            }
        }
        FoxDataResponseCollection responses = client.ExecuteMultiple(requests);
        AnsiConsole.MarkupLine("[blue]ResponseCollection.Diagnostics:[/] " + responses.Diagnostics);
        return responses;
    }
}
