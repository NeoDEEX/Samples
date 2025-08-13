# Logger Name Leveling Sample

이 예제는 Fox Logging의 로거 이름 레벨링 기능을 보여주는 예제 프로그램입니다. 로거 이름 레벨링에 대한 상세한 정보는 다음 문서를 참고하십시요.

* [로거 이름 레벨링](/doc/manual/logging/nameleveling.md)

---

이 예제에서 구현하는 어플리케이션은 인사(HR)와 회계(FI) 업무를 가상으로 구현하고 있으며 각 업무는 하위에 Salary, Etc, Closing 네임스페이스를 가지고 있습니다. 각 네임스페이스는 업무를 구현하는 서비스 클래스를 가지고 있고 각 클래스의 메서드는 디버깅을 위해 Verbase 수준의 로그 메시지를 기록합니다.

```cs
namespace HR.Salary;

public class SalaryService : HRBase
{
    public void Calc(string userId)
    {
        DebugLog("Calc() method start... userId={0}", userId);

        //.... 복잡한 계산을 수행...
        DebugLog("Calculating salary of user, {0}...", userId);

        DebugLog("Calc() method end...");
    }
}
```

로그를 기록하는 `HRBase` 클래스의 구현에서 `GetLogger` 메서드를 호출할 때 클래스의 타입 이름을 매개변수로 사용함에 주의하십시요. `SalaryService` 클래스의 경우, 로거로 요청되는 로거 이름은 `HR.Salary.SalaryService` 가 됩니다.

```cs
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
```

이제, 구성 설정에서 로거의 구성을 어떻게 수행하는가에 따라 로그가 기록되는 영역이 나뉘어 집니다. 예를 들어 `HR.Salary.SalaryService` 라는 이름을 가진 콘솔 로거를 다음과 같이 설정한다면,

```json
{
  "logging": {
    "loggers": {
      "HR.Salary.SalaryService": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxConsoleLoggerProvider"
        "filter": "Verbose"
      }
    }
  }
}
```

로그는 다음과 같이 기록될 것입니다.

```txt
V 2022-04-06 13:44:24.77788 [SalaryService] Calc() method start... userId=327856
V 2022-04-06 13:44:24.78274 [SalaryService] Calculating salary of user, 327856...
V 2022-04-06 13:44:24.78290 [SalaryService] Calc() method end...
```

로깅 영역을 확대하기 위해 로거의 이름을 `HR.Salary` 로 바꾼다면 기록되는 로그는 다음과 같습니다.

```txt
V 2022-04-06 13:44:24.77788 [SalaryService] Calc() method start... userId=327856
V 2022-04-06 13:44:24.78274 [SalaryService] Calculating salary of user, 327856...
V 2022-04-06 13:44:24.78290 [SalaryService] Calc() method end...
V 2022-04-06 13:44:24.78303 [SalaryBatch] BatchCalculate() method start...
V 2022-04-06 13:44:24.78307 [SalaryBatch] Batch calculating salary of all user...
V 2022-04-06 13:44:24.78311 [SalaryBatch] BatchCalculate() method end...
```

위 결과에서 인사 업무의 일부 혹은 전부에 대해 로그가 기록되지만 회계 업무의 로그는 기록되지 않음에도 주목하십시요. 회계 업무에 대해서도 로그를 기록하도록 하려면 새로운 로거를 추가하고 이름을 `FI` 로 시작하도록 구성 설정을 변경하면 됩니다.

또, 로거 영역에 따라 다른 로거에 로그가 기록되도록 할 수도 있습니다. 예를 들어 HR.Salary 네임스페이스는 콘솔에, HR.Etc 네임스페이스는 디버거에 로그를 기록하도록 설정하는 것도 가능합니다.

```json
{
  "logging": {
    "loggers": {
      "HR.Salary": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxConsoleLoggerProvider",
        "filter": "Verbose"
      },
      "HR.Etc": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxDebugLoggerProvider"
        "filter": "Verbose"
      }
    }
  }
}
```
