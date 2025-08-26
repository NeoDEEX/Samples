# NeoDEEX Extensions for MongoDB

MongoDB 를 위한 NeoDEEX 의 확장 기능 라이브러리입니다. NeoDEEX 는 전통적으로 RDBMS 에 대한 지원을 포함하고 있지만 MongoDB 와 같은 NoSQL 데이터베이스에 대한 지원은 포함하지 않습니다. 예를 들어, [Fox DB Access 기능](https://neodeex.github.io/doc/dbaccess/)은 ADO.NET 을 기반으로 하는 RDBMS 에 대한 데이터 액세스에 대해 다양한 기능을 제공합니다만 MongoDB Driver 는 ADO.NET 기반의 인터페이스를 제공하지 않기 때문에 `FoxDbAccess` 를 통해 MongoDB 에 접근하는 기능을 제공하기 어렵습니다.

하지만 MongoDB 는 매우 널리 사용되는 NoSQL 데이터베이스이므로 NeoDEEX 에서도 MongoDB 를 활용할 부분이 존재합니다. 예를 들어 이 라이브러리에 포함된 `DbProfileLogger` 는 [Fox DB Profile](https://neodeex.github.io/doc/dbaccess/dbprofile/overview/) 이 생성하는 정보를 MongoDB 에 저장하는 커스텀 로거 입니다. 이 외에도 MongoDB 에 기록된 DB Profile 정보를 조회하기 위한 DTO 클래스(`DbProfileInfo`) 등의 기능을 포함합니다.

* NeoDEEX.Extensions.Mongodb

    Fox DB Profile 을 MongoDB 에 기록하기 위한 로거와 DTO 클래스 등을 포함하는 라이브러리 입니다.

* dbprofile_gen_app

    NeoDEEX.Extensions.Mongodb 라이브러리를 사용하여 로거를 사용하여 MongoDB 에 Fox DB Profile 정보를 기록하고 DTO 클래스를 사용하여 MongoDB 에서 기록된 Fox DB Profile 정보를 읽어들이는 예제 Console 앱 입니다.

## DB Profile MongoDB Logger

`DbProfileLogger` 클래스는 Fox DB Profile 정보를 MongoDB 에 기록하기 위한 로거 이며 `DbProfileLoggerProvider` 클래스는 `DbProfileLogger` 에 대한 로거 프로바이더를 구현합니다. `DbProfileLogger` 는 수집된 Fox DB Profile 정보를 일정 주기마다 MongoDB 에 기록합니다. `DbProfileLogger` 에 대한 설정 예제는 다음과 같습니다.

```json
"logging": {
  "loggers": {
    "DbProfileLogger": {
      "filter": "Verbose",
      "providerType": "NeoDEEX.Extensions.Mongodb.DbProfileLoggerProvider, NeoDEEX.Extensions.Mongodb",
      "properties": {
        "connectionString": "connectionStrings:MongoDB",
        "userSecrets": "true",
        "database": "test_db",
        "collection": "dbprofile",
        "flushInterval": 1000,
        "diagnostics": "false"
      }
    }
  }
}
```

`connectionString` 로거 속성은 MongoDB 에 대한 연결 문자열입니다. `userSecrets` 로거 속성이 `true` 로 주어지면 `connectionString` 은 user-secrets 에 대한 경로 값으로 해석되며 user-secrets 에서(`FoxDatabaseConfig.ExternalSection` 속성) 연결 문자열을 읽습니다. `database` 로거 속성은 MongoDB 의 데이터베이스 이름이며 `collection` 로거 속성은 Fox DB Profile 정보를 기록할 컬렉션 이름입니다. 

DB Profile 정보는 데이터 액세스가 수행될 때마다 수집되므로 많은 양의 데이터가 발생됩니다. `DbProfileLogger` 는 발생한 DB Profile 정보 곧바로 기록하면 성능 저하가 발생할 수 있습니다. 따라서 `DbProfileLogger` 는 발생한 DB Profile 정보를 곧바로 기록하지 않고 큐(queue)에 수집하고 일정 시간이 지나면 별도의 스레드에서 큐에 존재하는 DB Profile 정보를 한번에 MongoDB 에 기록(`InsertMany` 메서드)합니다. 큐에 쌓인 데이터를 기록하는 디폴트 주기는 2000 msec(2초)이며 `flushInterval` 로거 속성으로 제어할 수 있습니다. `diagnostics` 로거 속성은 로거의 작동에 문제가 발생했을 때 진단을 목적으로 `true` 로 지정되면 `DbProfileLogger` 는 큐를 사용하지도 않고 동기적으로 곧바로 1건씩 MongoDB 에 기록합니다(`InsertOne` 메서드). 이렇게 함으로써 발생하는 문제점을 파악하기 쉬워 집니다.

## DbProfileInfo 클래스

`DbProfileInfo` 클래스는 MongoDB 에 기록된 Fox DB Profile 정보를 읽고 쓰는데 사용할 수 있는 DTO(Data Transfer Object) 타입입니다. `DbProfileLogger` 도 수집된 DB 프로파일 정보를 `DbProfileInfo` 객체로 전환하여 MongoDB 에 기록합니다.

```cs
var client = new MongoClient(connectionString);
var collection = client.GetDatabase("your_db").GetCollection<DbProfileInfo>("collection name");
var filter = Builders<DbProfileInfo>.Filter.And(
    Builders<DbProfileInfo>.Filter.Gte(d => d.Timestamp, from.ToUniversalTime()),
    Builders<DbProfileInfo>.Filter.Lte(d => d.Timestamp, to.ToUniversalTime())
);
var documents = collection.Find(filter).SortByDescending(d => d.Timestamp).Limit(1000).ToList();
foreach(DbProfileInfo doc in documents)
{
    // ... DbProfileInfo 객체 활용
}
```

---
