
using NeoDEEX.ServiceModel.Biz;
using System.Data;

namespace client_app;

// 예제용 간단한 팩터리 클래스
internal class SimpleBizRequestFactory(bool useDiagMode = false) : FoxBizRequestFactory
{
    public bool DiagnosticsMode { get; set; } = useDiagMode;

    public FoxBizRequestDiagnostics Diagnostics { get; set; } = FoxBizRequestDiagnostics.ServiceLog;

    public override FoxBizRequest Create(string classId, string methodId, IDictionary<string, object?>? parameters = null)
    {
        FoxBizRequest request = base.Create(classId, methodId, parameters);
        if (this.DiagnosticsMode == true)
        {
            request.Diagnostics = this.Diagnostics;
        }
        else
        {
            request.Diagnostics = FoxBizRequestDiagnostics.None;
        }
        return request;
    }
}
