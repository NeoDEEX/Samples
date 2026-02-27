# Miniaml Web API example

ASP.NET Core Minimal API 는 최소한의 종속성으로 HTTP API를 만들도록 설계되었습니다.
최소한의 파일과 종속성, 기능만을 포함하기 때문에 [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview) 과 같은 server-less microservice 에 사용하기 적합합니다.
상세한 내용은 [Minimal API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api) 관련 문서를 참고 하십시요.

NeoDEEX 5.x 부터는 Minimal API 를 지원하기 위해 `NeoDEEX.ServiceModel.WebApi` 패지키에서 다양한 디스패치 확장 메서드(extension method)를 제공합니다. 이 예제는 Minimal AP 환경에서 Fox Biz/Data Service Web API 를 추가하는 예를 보여 줍니다.

```cs
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.ConfigureBizService();

// FoxBizService 를 위한 MapPost 호출
app.MapPost("/api/bizservice/{action}", async (HttpRequest httpRequest) =>
{
    return await httpRequest.DispatchBizService();
});

// FoxDataService 를 위한 MapPost 호출
app.MapPost("/api/dataservice/{action}", async (string action, HttpRequest httpRequest) =>
{
    return await httpRequest.DispatchDataService();
});

app.Run();
```

이 예제와 관련된 상세한 문서는 [Getting Started: Minimal Web API](https://neodeex.github.io/doc/webapi/getting_started/minimal_api/) 를 참고 하십시요.

---
