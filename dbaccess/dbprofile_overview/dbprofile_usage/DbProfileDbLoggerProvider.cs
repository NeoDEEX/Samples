using NeoDEEX.Diagnostics;
using NeoDEEX.Diagnostics.Loggers;

namespace dbprofile_usage;

//
// FoxDbLoggerProvider 를 사용한 DbProfile 로거 프로바이더 구현
//
public class DbProfileDbLoggerProvider(string loggerName, FoxLoggerPropertyCollection properties)
    : FoxDbLoggerProviderBase(loggerName, properties)
{
    protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
    {
        DbProfileDbLogger logger = new(loggerName);
        // FoxDbLogger 가 기본적으로 제공하는 속성들을 그대로 사용합니다.
        SetStandardProperties(logger, properties, true);
        return logger;
    }
}
