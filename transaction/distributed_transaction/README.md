# Distributed Transacion Example

Fox Transactions 기능은 기본적으로 분산 트랜잭션을 사용합니다. 로컬 트랜잭션에 비해 분산 트랜잭션은 구성이 복잡하며 상대적으로 오버헤드가 큽니다. 그럼에도 System.Transactions 네임스페이스가 제공하는 환경 트랜잭션(ambient transaction)을 사용하면 트랜잭션 처리의 복잡도를 크게 줄일 수 있습니다.

분산 트랜잭션에 대한 기본적인 정보와 Fox Transactions 에서 분산 트랜잭션이 어떻게 활용되는지에 대해서 다음 문서를 참고 하십시요.

* [분산 트랜잭션 개요](https://neodeex.github.io/doc/transaction/distributed_transaction/)

이 예제를 통해 전통적인 분산 트랜잭션 패턴 코드와 Fox Transactions에서 동일한 처리를 수행하는 방법을 비교할 수 있습니다. 또한 이 예제는 전통적인 로컬 트랜잭션 처리 예제와 Fox Transactions를 사용한 로컬 트랜잭션 처리 예제를 포함하고 있습니다.

---
