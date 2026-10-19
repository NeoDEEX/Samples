using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Client;
using NeoDEEX.ServiceModel.Client.Biz;
using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using Spectre.Console;

namespace client_app;

internal static class BizServiceTest
{
    public static void DoTest()
    {
        //SimpleExceptionHandlingSample();
        //DoNotThrowException_ExecuteSample();
        //DoNotThrowException_ExecuteMultipleSample();
        ContinueOnErrorSample();
    }

    // 전형적이고 기본적인 예외 처리 예제.
    static void SimpleExceptionHandlingSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]Service Error Information Sample...[/]");
        try
        {
            FoxBizServiceClient client = new("/api/Bizservice");
            // 다음 호출은 항상 FoxRestClientException 을 발생시킴.
            FoxBizRequest request = new("BizLogic", "ErrorMethod");
            _ = client.Execute(request);
        }
        catch (FoxRemoteServiceException ex)
        {
            // Fox Biz/Data Service 클라이언트는 서비스에서 반환한 오류 정보를 FoxRemoteServiceException 타입으로 래핑하여
            // 예외를 유발하며, 서비스로부터 반환된 FoxServiceErrorInfo 객체는 ErrorInfo 속성을 통해 접근할 수 있음.
            AnsiConsole.MarkupLine($"[red]Biz Service Error!: {ex.Message}[/]");
            ex.ErrorInfo?.Dump();
        }
    }

    static void DoNotThrowException_ExecuteSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false ExecuteXXX Sample...[/]");
        FoxBizServiceClient client = new("/api/bizservice");
        FoxBizRequest request = new("BizLogic", "ErrorMethod") { ThrowException = false };
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        FoxBizResponse response = client.Execute(request);
        if (!response.Success)
        {
            AnsiConsole.MarkupLine($"[red]Biz Service Error![/]: {response.ErrorInfo?.Message}");
            response.ErrorInfo?.Dump();
        }
    }

    // ThrowException = false 인 상황에서 다중 쿼리를 수행하는 ExecuteMultiple 호출 예제
    static void DoNotThrowException_ExecuteMultipleSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false ExecuteMultiple Sample...[/]");
        FoxBizServiceClient client = new("/api/bizservice");
        FoxBizRequestCollection requests = CreateRequestCollection();
        requests.ThrowException = false;
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        FoxBizResponseCollection responses = client.ExecuteMultiple(requests);
        if (!responses.Success)
        {
            AnsiConsole.MarkupLine($"[red]Biz Service Error in processing request[[{responses.ErrorInfo.Index}]][/]");
            responses.ErrorInfo?.Dump();
        }
    }

    // ContinueOnError = true 인 상황에서 다중 쿼리를 수행하는 ExecuteMultiple 호출 예제
    static void ContinueOnErrorSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ContinueOnError = true ExecuteMultiple Sample...[/]");
        FoxBizServiceClient client = new("/api/bizservice");
        FoxBizRequestCollection requests = CreateRequestCollection();
        requests.ThrowException = false;
        requests.ContinueOnError = true;
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        // ContinueOnError = true 이므로, 오류가 발생한 요청 이후의 요청도 계속 수행됨.
        FoxBizResponseCollection responses = client.ExecuteMultiple(requests);
        for (int i = 0; i < responses.Count; i++)
        {
            FoxBizResponse response = responses[i];
            AnsiConsole.MarkupLine($"[blue]Request[[{i}]]:[/] {(response.Success ? "[blue]Success[/]" : "[red]Failure[/]")}");
            if (response.Success)
            {
                AnsiConsole.MarkupLine($"[yellow]RESULT:[/] [gray]{response.Result}[/]");
            }
            else
            {
                response.ErrorInfo?.Dump();
            }
        }
    }

    static FoxBizRequestCollection CreateRequestCollection()
    {
        FoxBizRequest request1 = new("BizLogic", "Echo");
        request1.Parameters["input"] = "Hello World!";
        FoxBizRequest request2 = new("BizLogic", "ErrorMethod");
        FoxBizRequestCollection requests = [request1, request2];
        return requests;
    }

}
