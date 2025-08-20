using NeoDEEX.Data;
using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Diagnostics;
using NeoDEEX.Diagnostics.Loggers;

namespace dbprofile_usage;

//
// FoxDbLogger 를 사용한 DbProfile 로거 구현
//
// 이 예제는 간단한 Console 어플리케이션이기 때문에 FoxDbLogger 의 동시성 기능을 `동기`(Synchronous)로 사용합니다.
// FoxDbLogger 동시성을 설정하기 위해 로거 구성 설정에서 'Concurrency` 속성을 'Synchronous` 혹은 'TPL` 로 설정할 수 있습니다.
// 비동기(TPL) 로 설정할 경우 로그가 별도의 스레드 풀을 사용하여 비동기적으로 기록되기 때문에 로그가 기록되기 전에
// 콘솔 어플리케이션이 종료될 수 있습니다.
//
public class DbProfileDbLogger(string loggerName) : FoxDbLoggerBase(loggerName)
{
    // 사용할 FoxQuery 에 대한 매개변수 인자값을 반환합니다.
    protected override object GetParameterObject(FoxLogEntry logEntry)
    {
        // FoxLogEntry의 Data 속성은 FoxDbProfileInfo 타입입니다.
        if (logEntry.Data is not FoxDbProfileInfo info)
        {
            throw new InvalidOperationException("FoxLogEntry.Data is not of type FoxDbProfileInfo.");
        }
        // Dictionary 인자값을 생성하여 반환합니다.
        // (info 컬럼의 Jsonb 타입은 .foxml 파일 내에서 명시되어 있습니다.)
        Dictionary<string, object?> parameters = new()
        {
            { "log_id", info.InfoId },
            { "log_time", info.Timestamp.ToLocalTime() },
            { "info", info.ToJson() }
        };
        return parameters;
    }

    // SQL 명령을 사용하지 않을 것이므로 예외를 발생시킵니다.
    protected override FoxDbParameterCollection GetParameterCollection(FoxDbAccess dbAccess, FoxLogEntry logEntry)
    {
        throw new NotSupportedException("ProfileDbLogger does not support sql text commands. Use FoxQuery instead.");
    }

}
