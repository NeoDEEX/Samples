# FoxEventLogLogger 예제

`FoxEventLogLogger` 를 사용하여 로그를 기록하는 예제 입니다.

> :warning: `FoxEventLogLogger` 는 Windows 이벤트 로거를 사용하기 때문에 Windows 플랫폼에 의존적입니다. 따라서 Linux 와 같은 환경에서는 사용할 수 없습니다.

`FoxEventLogLogger` 는 초기화 진행 시 이벤트 로그 이름이나 이벤트 로그 소스를 지정할 때 해당 이벤트 로그나 소스가 존재하지 않는 경우 `FoxEventLogger` 는 이를 생성하려고 시도합니다. 이 때 필요한 권한(관리자 권한)이 없는 경우 보안 오류가 발생할 수 있습니다.

```txt
Unhandled exception. NeoDEEX.FoxException: 권한이 부족하여 이벤트 로그를 액세스 하거나 생성할 수 없습니다.
```

이러한 문제를 피하기 위해서는 미리 이벤트 로그 나 이벤트 소스를 관리자 권한 하에서 작성하는 것이 좋습니다. 이렇게 하면 실제로 이벤트 로그를 남기는 어플리케이션은 관리자 권한이 없더라도 이벤트 로그에 로그를 기록할 수 있습니다. 이벤트 로그 및 이벤트 소스 생성은 PowerShell 명령어 [`New-EventLog`](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/new-eventlog?view=powershell-5.1) 를 사용하십시요.

다음 PowerShell 명령어는 이 예제를 구동시키기 위해 필요한 명령입니다. PowerShell을 관리자 권한으로 구동 시키고 다음 명령어를 수행하면 예제 구동에 필요한 이벤트 로그와 이벤트 소스를 등록하게 됩니다.

```ps
New-EventLog -Log Application -Source NeoDEEX
New-EventLog -Log NeoDEEX5 -Source EventLogSample
```

---
