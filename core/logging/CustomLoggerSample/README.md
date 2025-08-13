# Custom Logger Sample

`FoxLoggerBase` 클래스와 `FoxLoggerProviderBase` 클래스를 사용하여 사용자 정의 로거를 작성 하는 예제 입니다.

## 개요

이 예제 코드는 로그 메시지를 콘솔에 표시함과 동시에 파일에도 기록하는 `CustomLogger` 클래스를 작성하는 예제 입니다. `CustomLogger` 클래스는 콘솔에 표시하기 위해 콘솔 로거의 이름과 텍스트 파일 로거의 이름을 구성 설정을 통해 지정할 수 있습니다.

```json
{
  "logging": {
    "filter": "Verbose",
    "providers": {
      "CustomProvider": {
        "providerType": "LoggerLib.CustomLoggerProvider, LoggerLib",
        "properties": {
          "console": "ConsoleLogger",
          "file": "TextFileLogger"
        }
      }
    },
    "loggers": {
      "CustomLogger": {
        "providerName": "CustomProvider"
      },
      "ConsoleLogger": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxConsoleLoggerProvider"
      },
      "TextFileLogger": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxTextFileLoggerProvider",
        "properties": {
          "fileprefix": "Sample",
          "directory": "~/Logs"
        }
      }
    }
  }
}
```

로깅 구성 설정에서 이러한 설정을 읽어들이는 역할은 `CustomLoggerProvider` 클래스에 의해 수행됩니다. `CustomLoggerProvider` 클래스는 `FoxLogManager` 가 전달해 주는 `FoxLoggerPropertyCollection` 객체로부터 구성 설정 속성을 읽을 수 있습니다.

```cs
protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
{
    CustomLogger logger = new(loggerName);
    foreach(string key in properties)
    {
        string value = properties[key];
        if (String.Compare(key, "console", StringComparison.OrdinalIgnoreCase) == 0)
        {
            logger.ConsoleLogerName = value;
        }
        else if (String.Compare(key, "file", StringComparison.OrdinalIgnoreCase) == 0)
        {
            logger.FileLoggerName = value;
        }
        else
        {
            FoxLoggerProviderBase.ThrowPropertyNameException(logger, key);
        }
    }
    return logger;
}
```

## 고려 사항

이 예제의 `CustomLogger` 와 같이 다른 로거를 획득하여 로깅을 수행하는 경우, `GetLogger` 호출에 주의해야 합니다. `FoxLogManager` 가 구성 설정을 읽어 초기화를 완료하기 전에 `GetLogger` 혹은 `GetLoggerProvider` 호출은 `InvalidOperationException` 예외가 발생할 수 있습니다. 특히, `FoxLoggerProviderBase.OnCreateLogger` 메서드 내에서 `GetLogger` 메서드의 호출은 피해야 합니다. 따라서 `CustomLogger` 클래스는 최초의 로깅 호출이 발생할 때 `GetLogger` 메서드를 호출하여 필요한 로거를 획득하고 있습니다.

```cs
private void EnsureLogger()
{
    if (_consoleLogger == null)
    {
        if (this.ConsoleLogerName != null)
        {
            IFoxLog log = FoxLogManager.GetLogger(this.ConsoleLogerName);
            // ... 코드 생략 ...
            _consoleLogger = log;
        }
    }

    // ... 코드 생략 ...
}

protected override void WriteLogMessage(string source, FoxLogLevel level, string? message)
{
    EnsureLogger();
    _consoleLogger?.Write(source, level, message);
    _fileLogger?.Write(source, level, message);
}
```

---
