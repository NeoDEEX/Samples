using NeoDEEX.Diagnostics;

namespace dbprofile_usage;

public class DbProfileLoggerProvider(string name, FoxLoggerPropertyCollection properties)
    : FoxLoggerProviderBase(name, properties)
{
    protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
    {
        DbProfileLogger logger = new(loggerName);
        return logger;
    }
}
