# UseTypeInfo property sample

Fox Web API 의 JSON 직렬화에서 `object` 타입 값에 대한 타입 정보를 JSON 에 기록할지 여부를 제어하는 `UseTypeInfo` 속성 사용 예제입니다. 이 예제는 Visual Studio Code 환경에서 작성되었으므로 Visual Studio Solution 파일이 존재하지 않습니다.

`UseTypeInfo` 속성은 Fox Web API 에서 제공하는 `JsonConveter` 들이 제공하는 속성으로 이 속성 값이 `true`(디폴트)이면 `object` 타입 값에 대한 타입 정보를 JSON 에 기록하고, `false`이면 기록하지 않습니다.

```cs
FoxDictionaryConverter converter = new() { UseTypeInfo = true };
```

이렇게 `UseTypeInfo` 속성을 설정한 `JsonConverter` 들을 `JsonSerializerOptions` 에 추가하여 사용하면, `object` 타입 값에 대한 타입 정보 기록 여부를 제어할 수 있습니다.

```json
{
  "key1": {
    "$type": "decimal",
    "value": 123.45
  },
  "key2": {
    "$type": "double",
    "value": 123.45
  },
  "key3": {
    "$type": "float",
    "value": 123.45
  }
}
```

하지만 `JsonSerializerOptions` 객체를 매번 생성하여 사용하는 경우 성능 저하가 발생할 수 있기 때문에 Fox Web API 에서 제공하는 `GetJsonSerializerOptions` 메서드를 사용하여 타입별로 캐시된 `JsonSerializerOptions` 옵션을 재사용하는 것이 좋습니다.

```cs
JsonSerializerOptions options = typeof(Dictionary<string, object>).GetJsonSerializerOptions(true)
    ?? JsonSerializerOptions.Web;
```

---
