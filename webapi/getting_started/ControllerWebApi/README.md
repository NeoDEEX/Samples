# MVC Controller based Web API example

NeoDEEX 5.x 는 이전 버전과 마찬가지로 ASP.NET Core 의 컨트롤러 기반 Web API 프레임워크를 통해 Fox Biz/Data Service Web API 를 제공할 수 있습니다.

`Main` 메서드에 다음과 같이 `AddFoxWebApiControllers` 메서드를 호출하면 Fox Biz/Data Service 를 위한 Web API 컨트롤러가 추가되고 설정됩니다.

```cs
builder.Services.AddControllers().AddFoxWebApiControllers();
```

또한 `NeoDEEX.ServiceModel.WebApi` 패키지를 통해 제공되는 `FoxServiceResultAttribute`, `FoxAuthorizeAttribute`, `FoxFromBodyAttribute` 등 특성을 사용하여 Fox Biz/Data Service 기반의 새로운 Web API 를 작성하거나 기존 Web API 를 커스터마이징이 가능합니다.

이 예제와 관련된 상세한 문서는 [Getting Started: Controller-based Web API](https://neodeex.github.io/doc/webapi/getting_started/mvc_api/) 를 참고 하십시요.

---
