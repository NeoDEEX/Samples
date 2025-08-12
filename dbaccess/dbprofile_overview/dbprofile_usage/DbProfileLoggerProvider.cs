using NeoDEEX.Diagnostics;

namespace dbprofile_usage;

public class DbProfileLoggerProvider(string name, FoxLoggerPropertyCollection properties)
    : FoxLoggerProviderBase(name, properties)
{
    protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
    {
        DbProfileLogger logger = new(loggerName);
        string? connName = properties["ConnectionStringName"];
        if (String.IsNullOrEmpty(connName) == true)
        {
            throw new Exception("DbProfileLogger 를 사용하기 위해서는 ConnectionStringName 속성이 명시되어야 합니다.");
        }
        logger.ConnectionStringName = connName;
        return logger;
    }
}
