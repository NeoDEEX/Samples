# Accessing AppSettings 예제

[Fox Configuration](/doc/manual/configuration/) 기능을 사용하여 NeoDEEX 구성 설정 파일(`neodeex.config.json`) 내의 `"appSettings"` 섹션의 설정 값을 읽는 다양한 방법에 대한 예제 입니다.

이 예제에서는 `FoxConfigurationManager` 클래스의 `AppSettings` 속성을 사용하여 단순하게 키/값을 읽어 들이는 방법부터 설정값을 배열이나 객체와 같은 .net 타입으로 바인딩하여 접근하는 방법을 설명합니다.

`"appSettings"` 구성 설정 섹션에 대한 상세한 내용은 [AppSettings 접근](https://neodeex.github.io/doc/core/configuration/appsettings/) 문서를 참고 하십시요.

```cs
string setting1 = FoxConfigurationManager.AppSettings["setting1"];
bool setting3 = FoxConfigurationManager.AppSettings.GetValue<bool>("Setting3");
string subkey1 = FoxConfigurationManager.AppSettings["complex:subkey1"];
string element1 = FoxConfigurationManager.AppSettings["array:0"];
var complex = FoxConfigurationManager.AppSettings.Get<ComplexSetting>("complex");
var list1 = FoxConfigurationManager.AppSettings.Get<List<string>>("array");
```

```json
{
  // 어플리케이션 설정
  "appSettings": {
    "setting1": "value1",
    "setting2": "value2",
    "setting3": true,
    "setting4": 32,
    // 하위 객체를 갖는 설정
    "complex": {
      "subkey1": "value3",
      "subkey2": false,
      "stringList": [ "str1", "str2", "str3"],
      "subObject": {
        "name": "myName",
        "value": "myValue"
      }
    },
    "array": [ "str1", "str2", "str3"],
    "dic": {
      "key1": "value1",
      "key2": 2,
      "key3": true
    }
  }
}
```