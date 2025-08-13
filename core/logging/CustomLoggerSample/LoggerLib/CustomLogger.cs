using NeoDEEX.Diagnostics;
using NeoDEEX.Diagnostics.Loggers;

namespace LoggerLib;

public class CustomLogger : FoxLoggerBase
{
    private IFoxLog? _consoleLogger;
    private IFoxLog? _fileLogger;

    public string? FileLoggerName { get; internal set; }
    public string? ConsoleLogerName { get; internal set; }

    public CustomLogger(string name)
        : base(name)
    {
    }

    // 로거 생성 시점에서 GetLogger 호출은 구성설정 초기화 재진입 오류를 유발할 수 있다.
    // 따라서 GetLogger 호출을 최대한 지연한다(최초 로그 기록 호출 시점).
    private void EnsureLogger()
    {
        if (_consoleLogger == null)
        {
            // 주어진 이름의 콘솔 로거를 획득한다.
            if (this.ConsoleLogerName != null)
            {
                IFoxLog log = FoxLogManager.GetLogger(this.ConsoleLogerName);
                // 주어진 이름의 로거가 존재하지 않는다면 FoxDummyLogger가 반환될 것이다.
                if (log is not FoxConsoleLogger && log is not FoxDummyLogger)
                {
                    throw new ArgumentException($"The logger, {this.ConsoleLogerName}, is not supported.");
                }
                _consoleLogger = log;
            }
        }
        if (_fileLogger == null)
        {
            // 주어진 이름의 텍스트 파일 로거를 획득한다.
            if (this.FileLoggerName != null)
            {
                IFoxLog log = FoxLogManager.GetLogger(this.FileLoggerName);
                // 주어진 이름의 로거가 존재하지 않는다면 FoxDummyLogger가 반환될 것이다.
                if (log is not FoxTextFileLogger && log is not FoxDummyLogger)
                {
                    throw new ArgumentException($"The logger, {this.FileLoggerName}, is not supported.");
                }
                _fileLogger = log;
            }
        }
    }

    protected override void WriteLog(string source, FoxLogLevel level, object? data)
    {
        // 로그 데이터를 문자열로 변환하여 WriteLogMessage 메서드를 호출한다.
        this.WriteLogMessage(source, level, data?.ToString());
    }

    protected override void WriteLogMessage(string source, FoxLogLevel level, string? message)
    {
        EnsureLogger();
        _consoleLogger?.Write(source, level, message);
        _fileLogger?.Write(source, level, message);
    }
}
