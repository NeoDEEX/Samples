using NeoDEEX.Diagnostics;

namespace HR;

public class HRBase
{
    protected readonly IFoxLog _log;
    
    public HRBase()
    {
        _log = FoxLogManager.GetLogger(this.GetType());
    }

    protected void DebugLog(string fmt, params object[] args)
    {
        _log.WriteFormat(this.GetType().Name, FoxLogLevel.Verbose, fmt, args);
    }
}
