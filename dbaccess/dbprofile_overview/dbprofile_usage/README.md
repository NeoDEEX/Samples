# FoxDbProfile 활용 예제

이 예제는 Fox DB Access 에 포함된 Fox DB Profile 활용과 관련된 예제 코드로서 2개의 커스텀 로거를 포함합니다.

## DbProfileLogger

`FoxLoggerBase` 에서 파생된 Fox DB Profile 전용 로거(`DbProfileLogger`)는 핵심적인 기능만이 구현되어 있으므로 데이터베이스에 로그 항목을 기록하는 로거의 작동 방식을 이해하는데 도움이 되며 성능적으로 우수합니다.

## ProfileDbLogger

`FoxDbLoggerBase` 에서 파생된 Fox DB Profile 전용 로거(`ProfileDbLogger`)는 `FoxDbLoggerBase` 가 기본적으로 제공하는 트랜잭션에 대한 고려, FoxQuery 사용 가능, 전용 스레드 풀을 사용한 비동기화, 다른 데이터베이스로의 높은 이식성 등의 기능을 모두 활용합니다.

Fox DB Profile 활용에 대한 상세한 내용은 [FoxDbProfile 활용](https://neodeex.github.io/doc/dbaccess/dbprofile/using_dbprofile/)를 참고 하십시요.

---
