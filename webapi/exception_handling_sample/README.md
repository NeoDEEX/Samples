# Fox Biz/Data Service 예외 처리 예제

Fox Biz/Data Service 는 서비스(서버) 측에서 발생한 오류에 대한 상세한 정보를 클라이언트에게 전달하는 기능을 제공합니다. 이 예제는 Fox Web API 를 사용하여 Fox Biz/Data Service 를 호출하고 서비스(서버) 측에서 발생한 오류를 클라이언트에서 처리하는 방법을 보여줍니다.

Fox Biz/Data Service 의 예외 처리에 대한 상세한 내용은 다음 문서들을 참고 하십시오.

* [Fox Data Service 예외 처리](https://neodeex.github.io/doc/webapi/dataservice/adv_usage.md#exception_handling)
* [Fox Biz Service 예외 처리](https://neodeex.github.io/doc/webapi/bizservice/adv_usage.md#exception_handling)

이 예제 코드에서는 다음과 같은 예제를 확인할 수 있습니다.

* 전형적이고 간단한 예외 처리 방법
* `ThrowException` 속성을 false 로 설정하고 단일 쿼리를 호출할 때 오류 처리 방법
* `ThrowException` 속성을 false 로 설정하고 여러 쿼리를 호출할 때 오류 처리 방법
* `ContinueOnError` 속성 true 로 설정하고 호출할 때 오류 처리 방법
* 컬렉션 수준의 트랜잭션을 사용할 때 `ContinueOnError = true` 상황 테스트 코드
* 개별 Request 수준의 트랜잭션을 사용할 때 `ContinueOnError = true` 상황 테스트 코드

---
