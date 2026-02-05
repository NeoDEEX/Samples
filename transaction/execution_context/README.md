# Fox Transactions Execution Context Sample

Fox Transactions 의 작동 원리는 수행 프록시(Execution Proxy)를 통해 메서드 호출을 가로채고 전처리와 후처리를 통해 트랜잭션을 관리하는 것입니다. 이 과정에서 중요한 역할을 하는 것이 바로 수행 문맥(Execution Context)입니다. 수행 문맥은 Fox Transactions 에서 트랜잭션이 실행되는 환경을 정의하며 관련 정보들을 기록하고 있습니다.

Fox Tansactions 의 작동 방식과 수행 문맥에 관한 상세한 내용은 다음 문서를 참고 하십시요.

* [수행 문맥](https://neodeex.github.io/doc/transaction/execution_context/)

![Fox Transactions Execution Context](https://neodeex.github.io/doc/transaction/images/execution_context.png)

FoxExecutionContext 클래스는 수행 문맥을 나타내며, 다음과 같은 주요 속성들을 포함하고 있습니다:

* 호출 정보
* 트랜잭션 정보
* 기타 메타 정보

FoxExecutionContext 클래스에 대한 상세한 내용은 다음 문서를 참고 하십시요.

* [FoxExecutionContext 클래스](https://neodeex.github.io/doc/transaction/execution_context#fox_execution_context)

---
