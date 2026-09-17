
using NeoDEEX.ServiceModel.Data;
using System.Data;

namespace client_app;

// 예제용 간단한 팩터리 클래스
internal class SimpleRequestFactory(bool useDiagMode = false) : FoxDataRequestFactory
{
    public bool DiagnosticsMode { get; set; } = useDiagMode;

    public FoxDataRequestDiagnostics Diagnostics { get; set; } = FoxDataRequestDiagnostics.ServiceLog;

    public override FoxDataRequest Create(string? queryId, string? databaseName = null, IDictionary<string, object?>? parameters = null, DataSet? dataSet = null)
    {
        FoxDataRequest request = base.Create(queryId, databaseName, parameters, dataSet);
        if (this.DiagnosticsMode == true)
        {
            request.Diagnostics = this.Diagnostics;
        }
        else
        {
            request.Diagnostics = FoxDataRequestDiagnostics.None;
        }
        return request;
    }
}
