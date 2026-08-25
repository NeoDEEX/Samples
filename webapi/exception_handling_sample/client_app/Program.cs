using NeoDEEX.ServiceModel.Client;
using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using Spectre.Console;

namespace client_app;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Biz/Data Service Exception Handling Sample Client...[/]");

        ResetProductsTable();

        //SimpleExceptionHandlingSample();
        //DoNotThrowException_ExecuteXXXSample();
        //DoNotThrowException_ExecuteMultipleSample();
        //ContinueOnErrorSample();
        //DoNotThrowException_WithTransaction_Sample();
        ContinueOnError_WithEachTransaction_Sample();
    }

    static void ResetProductsTable()
    {
        AnsiConsole.MarkupLine("[blue]Reset Products Table...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequest request = new("sample.reset_test") { Operation = FoxDataOperations.ExecuteNonQuery };
        client.ExecuteNonQuery(request);
    }

    // 전형적이고 기본적인 예외 처리 예제.
    static void SimpleExceptionHandlingSample()
    { 
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]Service Error Information Sample...[/]");
        try
        {
            FoxDataServiceClient client = new("/api/dataservice");
            // 다음 호출은 항상 FoxRestClientException 을 발생시킴.
            FoxDataRequest request = new("sample.error_query_id");
            _ = client.ExecuteDataSet(request);
        }
        catch (FoxRemoteServiceException ex)
        {
            // Fox Biz/Data Service 클라이언트는 서비스에서 반환한 오류 정보를 FoxRemoteServiceException 타입으로 래핑하여
            // 예외를 유발하며, 서비스로부터 반환된 FoxServiceErrorInfo 객체는 ErrorInfo 속성을 통해 접근할 수 있음.
            AnsiConsole.MarkupLine($"[red]Data Service Error!: {ex.Message}[/]");
            ex.ErrorInfo?.Dump();
        }
    }

    // ThrowException = false 인 상황에서 단일 쿼리를 수행하는 ExecuteXXX 호출 예제
    static void DoNotThrowException_ExecuteXXXSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false ExecuteXXX Sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequest request = new("sample.error_query_id") { ThrowException = false };
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        FoxDataResponse response = client.ExecuteDataSet(request);
        if (!response.Success)
        {
            AnsiConsole.MarkupLine($"[red]Data Service Error![/]: {response.ErrorInfo?.Message}");
            response.ErrorInfo?.Dump();
        }
    }

    // ThrowException = false 인 상황에서 다중 쿼리를 수행하는 ExecuteMultiple 호출 예제
    static void DoNotThrowException_ExecuteMultipleSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false ExecuteMultiple Sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequestCollection requests = CreateRequestCollection();
        requests.ThrowException = false;
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        FoxDataResponseCollection responses = client.ExecuteMultiple(requests);
        if (!responses.Success)
        {
            AnsiConsole.MarkupLine($"[red]Data Service Error in processing request[[{responses.ErrorInfo.Index}]][/]");
            responses.ErrorInfo?.Dump();
        }
        // 예외가 발생하기 전의 수행 결과는 받아 볼 수 있다.
        responses[0].DumpProducts();
    }

    // ContinueOnError = true 인 상황에서 다중 쿼리를 수행하는 ExecuteMultiple 호출 예제
    static void ContinueOnErrorSample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ContinueOnError = true ExecuteMultiple Sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequestCollection requests = CreateRequestCollection();
        requests.ThrowException = false;
        requests.ContinueOnError = true;
        // 다음 호출은 서비스에서 반환된 오류 정보를 FoxServiceErrorInfo 객체로 반환함.
        // ContinueOnError = true 이므로, 오류가 발생한 요청 이후의 요청도 계속 수행됨.
        FoxDataResponseCollection responses = client.ExecuteMultiple(requests);
        for (int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            AnsiConsole.MarkupLine($"[blue]Request[[{i}]]:[/] {(response.Success ? "[blue]Success[/]" : "[red]Failure[/]")}");
            if (response.Success)
            {
                response.DumpProducts();
            }
            else
            {
                response.ErrorInfo?.Dump();
            }
        }
    }

    // ThrowException = false 인 상황에서 트랜잭션을 사용하는 ExecuteMultiple 호출 예제
    static void DoNotThrowException_WithTransaction_Sample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false in Transaction Sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequestCollection requests = CreateRequestCollectionForUpdate();
        // 디폴트 데이터베이스 연결 문자열 사용
        requests.DatabaseName = String.Empty;
        // 로컬 트랜잭션 사용 (Sqlite 는 로컬 트랜잭션만 지원)
        requests.Transaction = FoxDataTransactions.Local;
        // 예외를 발생하지 않도록 설정
        requests.ThrowException = false;
        // 트랜잭션이 사용되므로, 요청 중 하나라도 실패하면 전체 트랜잭션이 롤백됨.
        FoxDataResponseCollection responses = client.ExecuteMultiple(requests);
        AnsiConsole.MarkupLine($"[red]Responses.Count = {responses.Count}[/]");
        if (!responses.Success)
        {
            AnsiConsole.MarkupLine($"[red]Data Service Error in processing request[[{responses.ErrorInfo.Index}]][/]");
            responses.ErrorInfo?.Dump();
        }
        // 비록 첫번째 쿼리(insert_product)가 성공하였고 FoxDataResponse 객체의 Success 속성이 true 이지만, 트랜잭션이 롤백되었으므로 실제로는 데이터가 반영되지 않음.
        if (responses[0].Success)
        {
            AnsiConsole.MarkupLine($"[red]response[[0]].AffectedRows={responses[0].AffectedRows}[/]");
            AnsiConsole.MarkupLine("[red]However, the transaction was rolled back, so the data is not actually inserted.[/]\n");
        }
        // 트랜잭션이 롤백 되었나 확인.
        AnsiConsole.MarkupLine("[blue]Check if the transaction was rolled back...[/]");
        FoxDataRequest request4 = new("sample.get_all_products") { Operation = FoxDataOperations.ExecuteDataSet };
        FoxDataResponse response = client.ExecuteDataSet(request4);
        response.DumpProducts();
    }

    // ContinueOnError = true 인 상황에서 개별 Request 별로 트랜잭션을 사용하는 ExecuteMultiple 호출 예제
    static void ContinueOnError_WithEachTransaction_Sample()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]ThrowException = false in Transaction Sample...[/]");
        FoxDataServiceClient client = new("/api/dataservice");
        FoxDataRequestCollection requests = CreateRequestCollectionForUpdate();
        foreach(FoxDataRequest request in requests)
        {
            request.DatabaseName = String.Empty;
            request.Transaction = FoxDataTransactions.Local;
        }
        // 예외를 발생하지 않도록 설정
        requests.ThrowException = false;
        requests.ContinueOnError = true;
        // 개별 Request 별로 트랜잭션을 사용하므로 3개의 요청이 모두 수행된다.
        FoxDataResponseCollection responses = client.ExecuteMultiple(requests);
        for(int i = 0; i < responses.Count; i++)
        {
            FoxDataResponse response = responses[i];
            if (response.Success)
            {
                AnsiConsole.MarkupLine($"[blue]Request[[{i}]] Success: AffectedRows={response.AffectedRows}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Request[[{i}]] Failure: Data Service Error in processing request[[{i}]][/]");
                response.ErrorInfo?.Dump();
            }
        }
        AnsiConsole.WriteLine();
        // 각 Request 별로 트랜잭션이 수행되므로 첫번째 요청(insert_product)과 세번째 요청(update_product)은 성공함.
        // Products 테이블의 내용을 확인하기 위한 조회.
        AnsiConsole.MarkupLine("[blue]Check if the updates take effect...[/]");
        FoxDataRequest request4 = new("sample.get_all_products") { Operation = FoxDataOperations.ExecuteDataSet };
        FoxDataResponse response4 = client.ExecuteDataSet(request4);
        response4.DumpProducts();
    }

    static FoxDataRequestCollection CreateRequestCollection()
    {
        FoxDataRequest request1 = new("sample.get_all_products") { Operation = FoxDataOperations.ExecuteDataSet };
        FoxDataRequest request2 = new("sample.error_query_id") { Operation = FoxDataOperations.ExecuteNonQuery };
        FoxDataRequest request3 = new("sample.get_product_by_id")
        {
            Operation = FoxDataOperations.ExecuteDataSet,
            Parameters = { { "product_id", "P001" } }
        };
        FoxDataRequestCollection requests = [request1, request2, request3];
        return requests;
    }

    static FoxDataRequestCollection CreateRequestCollectionForUpdate()
    {
        FoxDataRequest request1 = new("sample.insert_product")
        {
            Operation = FoxDataOperations.ExecuteNonQuery,
            Parameters = { { "product_id", "PROD999" }, { "product_name", "Test Error Product" }, { "unit_price", 9.99 } }
        };
        FoxDataRequest request2 = new("sample.error_query_id") { Operation = FoxDataOperations.ExecuteNonQuery };
        FoxDataRequest request3 = new("sample.update_product")
        {
            Operation = FoxDataOperations.ExecuteNonQuery,
            Transaction = FoxDataTransactions.Local,
            Parameters = { { "product_id", "P001" }, { "product_name", "NEW PRODUCT NAME" }, { "unit_price", 1.11 } }
        };
        FoxDataRequestCollection requests = [request1, request2, request3];
        return requests;
    }
}
