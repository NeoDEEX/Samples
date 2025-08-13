# 로깅 문제 해결 예제 코드

로깅 문제 해결 문서의 예제 코드들 입니다.

또한 이 예제 코드를 통해 로깅 설정 변경을 즉시 감지하고 로그가 바뀌는 테스트도 수행할 수 있습니다. 이 테스트를 수행하기 위해서는 `neodeex.config.json` 파일을 다음과 같이 설정하고 프로그램을 구동하십시요.

```json
{
  "logging": {
    "loggers": {
      "HR.Salary.SalaryService": {
        "providerType": "NeoDEEX.Diagnostics.Loggers.FoxConsoleLoggerProvider",
        "filter": "Verbose"
      }
    }
  }
}
```

예제가 구동하면 매초 3개의 로그가 콘솔에 나타납니다. 예제를 구동 시킨 상태에서 위 구성 설정의 `"filter"` 속성 값을 `Information` 으로 변경하고 저장합니다. 이제 매초 1개의 로그만이 나타나게 됩니다.

```log
Fox Logging Trouble Shooting Sample...
Press Any Key to Stop...
V 2022-04-18 17:09:10.85293 [HR.Salary.SalaryService] Calc() method start... userId=TestUser
I 2022-04-18 17:09:10.85805 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
V 2022-04-18 17:09:10.85833 [HR.Salary.SalaryService] Calc() method end...
V 2022-04-18 17:09:11.87174 [HR.Salary.SalaryService] Calc() method start... userId=TestUser
I 2022-04-18 17:09:11.87182 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
V 2022-04-18 17:09:11.87201 [HR.Salary.SalaryService] Calc() method end...

==> neodeex.config.json 파일 변경 ----
I 2022-04-18 17:09:15.90711 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
I 2022-04-18 17:09:16.92180 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
I 2022-04-18 17:09:17.92352 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
I 2022-04-18 17:09:18.93712 [HR.Salary.SalaryService] Calculating salary of user, TestUser...
```

> 주의) 수정할 `neodeex.config.json` 파일은 수행 파일과 동일한 폴더(`bin/Debug/net6.0` 폴더)에 존재하는 파일이어야 합니다.

---
