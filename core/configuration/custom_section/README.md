# Custom Configuration Section 예제

[Fox Configuration](https://neodeex.github.io/doc/core/configuration/) 기능을 사용하여 NeoDEEX 구성 설정 파일(`neodeex.config.json`)에 사용자 정의 구성 설정 섹션을 작성하고 관리하는 예제 입니다.

`FoxConfigurationSection` 클래스와 `FoxConfiguration<T>` 클래스를 사용하면 사용자 정의 섹션을 손쉽게 읽어 들이고 관리할 수 있습니다. 이 두 클래스에 대한 상세한 내용은 [커스텀 섹션 작성](https://neodeex.github.io/doc/core/configuration/customsection/) 문서를 참고 하십시요.

```json
{
  "mySettings": {
    "stringProp": "StringValue1",
    "boolProp": false,
    "stringList": [ "str1", "str2", "str3", "str4" ],
    "subObject": {
      "name": "myName",
      "value":  "myValue"
    }
  }
}
```

```cs
internal class MySettings
{

    public string? StringProp { get; set; }
    public bool? BoolProp { get; set; }
    public List<string> StringList { get; set; } = new List<string>();
    public SubType? SubObject { get; set; }
}
```

```cs
var section = new FoxConfiguration<MySettings>("mySettings");
MySettings mySettings = section.Section;
if (mySettings.BoolProp == true)
{
    //... boolProp가 true로 설정됨
}
else
{
    //... boolProp가 false 이거나 명시되지 않음.
}

```
