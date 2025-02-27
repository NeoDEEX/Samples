# JWT Authentication on Fox Biz/Data Service Web API

이 예제는 JWT 인증을 사용하여 Fox Web Service Web API 를 구현하는 방법을 보여줍니다.

## Overview

JWT 토큰은 발행자가 대상(subject)에 대한 정보를 JSON 으로 기록하고 이 내용 변경되지 않았음을 확인하도록 디지털 서명을 포함합니다. JWT 에 대한 기본 개념은 다음 문서를 참고하십시요.

<https://jwt.io/introduction>

Fox Biz/Data Service Web API 는 인증 문자열(auth string)이라 부르는 암호화된 NeoDEEX 토큰을 사용한 기본적이고 간단한 인증 방식을 제공합니다. 하지만 이 방식은 표준화되지 않은 방법이며 폐쇠된 환경에서만 사용이 가능합니다.

반면 JWT 는 [RFC 7519](https://datatracker.ietf.org/doc/html/rfc7519)에 정의된 오픈 표준으로서 공개 환경에서 많이 사용되기 때문에 기업 내부 시스템에서도 JWT 기반 인증을 적용하는 경우가 많습니다.

이 예제는 JWT Bearer 인증을 적용하여 Fox Biz/Data Service Web API 를 구현하는 방법에 대해 설명합니다.

## Fox Authentication & Authorization

Fox Biz/Data Service Web API 는 HTTP 헤더에 NeoDEEX 토큰이 존재하는지 여부에 따라 인증된 사용자의 서비스 호출 여부를 판단합니다. Fox Authentication 기능은 `UseFoxAuthentication` 메서드를 호출함으로써 설정할 수 있습니다.

```cs
var app = builder.Build();
app.UseFoxAuthentication();
```

`UseFoxAuthentication` 메서드는 ASP.NET Core 미들웨어(`FoxWebApiAuthentication` 클래스)를 추가하고 이 미들웨어는 `FoxRest-Authenticate` HTTP 헤더의 존재 여부와 이 헤더값의 유효한 NeoDEEX 토큰을 포함하는지 판단합니다. 유효한 NeoDEEX 토큰이 존재하는 경우, 인증된 사용자의 서비스 호출으로 간주하고 `FoxWebApiAuthentication` 클래스의 `SetAuthenticated` 정적 메서드를 호출하여 현재의(current) `HttpContext` 객체가 인증된 것으로 표시합니다.

인증된 사용자로부터의 호출만을 허용하는 권한 확인은 Fox Authorization 에 의해 수행됩니다. Web API 컨트롤러에 `FoxAuthorizeAttribute` 특성을 사용하면 인증되지 않은 사용자의 호출에 대해 `401 Unauthorized` 오류를 반환할 수 있습니다.

## JWT Bearer Authentication

JWT Bearer 인증을 수행하기 위해서는 클라이언트가 인증 서버에 사용자 Id/암호 등을 제시하면 JWT 토큰을 발행합니다. 클라이언트는 발급받은 JWT 토큰을 `Authorization` HTTP 헤더에 추가하여 Web API 서버를 호출해야 합니다. Web API 서버는 HTTP 헤더에서 유효한 JWT 토큰이 존재하는지 확인하여 인증을 수행합니다. 이 상황에서 인증서버는 발급자(issuer)가 되며 Web API 서버는 수신자(audience)가 됩니다.

개방 환경에서는 발급자와 수신자가 분리되는 것이 일반적이며 Goggle 이나 Microsoft 의 인증 서버가 발급자가 될 수 있습니다. 토큰 발급자는 토큰이 사용될 수 있는 수신자를 토큰에 포함되며 수신자는 토큰에 기록된 수신자에 자신이 포함되는지 확인합니다.

### Creating JWT token

이 예제와 같이 간단한 예제나 간단한 독립 어플리케이션에서는 발급자와 수신자가 동일하게 됩니다. 이 경우 OpenID 와 같은 표준적인 통신 방식을 사용하지 않으며 Web API 서버가 login 과 같은 API 에서 JWT 토큰을 발행합니다.

```cs
// Fox Web API 를 호출하기 위해 로그인을 호출하여 JWT 토큰을 발급합니다.
app.MapPost("/api/login", async (HttpContext context, RequestUserCredential credential) =>
{
    // 간단한 예제이므로 사용자 인증을 하드코딩으로 처리합니다.
    // 실제 앱에서는 DB, 디렉터리 등을 사용하여 사용자를 인증해야 합니다.
    if ((credential.UserId == "admin" && credential.Password == "admin")
        || (credential.UserId == "tester" && credential.Password == "test"))
    {
        // FoxUserInfoContext를 생성하고 NeoDEEX 토큰으로 변환합니다.
        FoxUserInfoContext ctx = new(credential.UserId);
        ctx["DEPT"] = "IT";
        string authString = FoxUserInfoContext.VersionIndependentSerialize(ctx);
        // JWT 토큰을 생성하고 반환합니다.
        string jwtToken = JwtHelper.CreateToken(credential.UserId, WebApiAudience, authString);
        await context.Response.WriteAsync(jwtToken);
    }
    else
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid userid or password");
    }
});
```

`login` API 는 클라이언트로부터 사용자 ID, 암호 등의 정보를 수신하여 인증을 수행하고 JWT 토큰을 생성하여 반환합니다. 이 때 사용자 정보를 포함하는 `FoxUserInfoContext` 객체를 생성하고 NeoDEEX 토큰으로 변환하여 JWT 토큰에 포함시킵니다.

`CreateToken` 메서드는 `System.IdentityModel.Tokens.Jwt` 패키지의 `JwtSecurityToken`, `Claim`, `JwtSecurityTokenHandler` 클래스를 활용하여 JWT 토큰 문자열을 생성하여 반환합니다.

```cs
// JWT 토큰을 생성하여 반환합니다.
public static string CreateToken(string subject, string? audience = null, string? authString = null, double expires = 120)
{
    SigningCredentials credentials = new(SecurityKey, SecurityAlgorithms.HmacSha256);

    List<Claim> claims = [
        new Claim(JwtRegisteredClaimNames.Sub, subject),
        // UniqueName 을 명시해야 Identity.Name 속성에서 사용자 Id를 가져올 수 있습니다.
        new Claim(JwtRegisteredClaimNames.UniqueName, subject),
    ];
    if (authString != null)
    {
        claims.Add(new Claim(FoxAuthStringClaimName, authString));
    }

    var token = new JwtSecurityToken(
        Issuer,
        audience,
        claims,
        expires: DateTime.Now.AddMinutes(expires),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

### Setting JWT Bearer Authentication

JWT Bearer 인증 설정을 위해서는 `AddAuthentication` 메서드 호출과 `AddJwtBearer` 메서드 호출을 수행해야 합니다. `AddJwtBearer` 메서드 호출 시 토큰 생성에 사용했던 Issuer, Audience, 서명용 키 등에 대한 검증을 수행하도록 `TokenValidationParameters` 객체를 설정해 주어야 합니다.

```cs
// JWT Bearer 인증을 추가합니다.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = JwtHelper.SecurityKey,
            ValidateIssuer = true,
            ValidIssuer = JwtHelper.Issuer,
            ValidateAudience = true,
            ValidAudience = WebApiAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
```

이제 실제로 JWT Bearer 인증을 수행하도록 `UseAuthentication` 메서드를 호출해야 합니다.

```cs
var app = builder.Build();
// JWT Bearer 인증을 수행하도록 합니다.
app.UseAuthentication();
// asp.net 권한 확인을 위한 호출. Fox Web API 에서는 불필요합니다. 
//app.UseAuthorization();
```

`UseAuthentication` 메서드는 `FoxUseAuthentication` 메서드와 비슷하게 인증 미들웨어를 구성하여 `Authorization` HTTP 헤더에 JWT 토큰이 존재하는지 확인하고 존재하는 경우 `AddJwtBearer` 메서드에 의해 설정된 검증 옵션을 사용하여 검증을 수행합니다.

검증에 성공한 경우 `HttpContext.User` 객체에 JWT 토큰에서 추출한 정보들을 채워넣습니다. JWT Bearer 인증이 성공한 경우, 다음 코드를 통해 NeoDEEX 토큰을 추출할 수 있습니다.

```cs
string? authString = context.User.Claims.FirstOrDefault(c => c.Type == JwtHelper.FoxAuthStringClaimName)?.Value;
```

NeoDEEX 토큰을 구할 수 있으므로 이로부터 `FoxUserInfoContext` 객체를 생성할 수 있으며 `SetAuthenticated` 메서드를 호출하여 NeoDEEX 인증을 설정할 수 있습니다.

`UseFoxAuthentication` 메서드는 HTTP 헤더에서 NeoDEEX 토큰을 찾으므로 적용할 수 없습니다. 대신 간단한 asp.net 미들웨어를 추가하여 JWT 토큰에서 NeoDEEX 토큰을 찾도록할 수 있습니다.

```cs
// JWT Bearer 인증을 수행하도록 합니다.
app.UseAuthentication();

// Fox Authentication 을 사용하지 않고 JWT 토큰으로부터 인증을 수행합니다.
//app.UseFoxAuthentication();
// JWT 토큰에서 NeoDEEX 토큰을 추출하기 위한 인증 문자열 추출하기 위한 미들웨어 추가
app.Use(async (context, next) =>
{
    if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated == true)
    {
        string? userId = context.User.Identity.Name;
        string? authString = context.User.Claims.FirstOrDefault(c => c.Type == JwtHelper.FoxAuthStringClaimName)?.Value;
        if (string.IsNullOrEmpty(authString) == false)
        {
            FoxUserInfoContext ctx = FoxUserInfoContext.VersionIndependentDeserialize(authString);
            context.SetAuthenticated(ctx);
        }
    }
    await next.Invoke();
});
```

Asp.net 미들웨어는 수행 순서가 중요하므로 위 미들웨어 추가는 `UseAuthentication` 메서드 호출 이후에 이루어져야 함에 유의하십시요.

## Test

인증된 사용자만이 Fox Biz/Data Service Web API 를 호출하도록 `neodeex.config.json` 파일을 확인합니다.

```json
{
  "$schema": "https://neodeex.github.io/doc/neodeex.config.schema.json",
  "webapiServer": {
    "useAuthorization": true
  }
}
```

### Unauthorized test

JWT 인증을 테스트하기 위해서 적절한 도구를 사용하여 Fox Biz Service Web API 를 호출해 봅니다.

```http
POST {{webapi_app_HostAddress}}/api/bizservice/execute
Accept: application/json
Content-Type: application/json

{
  "classId": "webapi_app.Biz.TestBizLogic",
  "methodId": "GetHello"
}
```

수행 결과는 `401 Unauthorized` 오류와 더불어 다음과 유사한 추가 정보를 반환할 것입니다.

```json
{
  "message": "현재 HTTP 문맥에 사용자 정보를 담는 유효한 FoxUserInfoContet 객체가 존재하지 않습니다.",
  "messageDetail": "NeoDEEX.ServiceModel.WebApi.FoxWebApiException: 현재 HTTP 문맥에 사용자 정보를 담는 유효한 FoxUserInfoContet 객체가 존재하지 않습니다.",
  "exceptionType": "NeoDEEX.ServiceModel.WebApi.FoxWebApiException",
  "stackTrace": "   at NeoDEEX.ServiceModel.WebApi.FoxWebApiExtensions.FoxSimpleAuthorize(HttpContext httpContext, Nullable`1 useAuthorization)\r\n   at NeoDEEX.ServiceModel.WebApi.Filters.FoxAuthorizeAttribute.OnResourceExecuting(ResourceExecutingContext context)",
  "errorCode": 401
}
```

### Getting JWT token

인증을 수행하기 위해서는 먼저 `login` API 를 호출하여 JWT 토큰을 발급받아야 합니다.

```http
POST {{webapi_app_HostAddress}}/api/login
Accept: application/json
Content-Type: application/json

{
  "userId": "tester",
  "password": "test"
}
```

`login` API 가 반환한 JWT 토큰은 다음과 같이 인코딩 되어 있을 것입니다.

```txt
eyJhbGciOiJIUzI1NiIsInR5cC ... 생략 ...
```

인코딩된 JWT 토큰의 내용을 확인하고자 한다면 <https://jwt.io/#debugger-io> 사이트에서 디코딩해 볼 수 있습니다.

```json
{
  "sub": "tester",
  "unique_name": "tester",
  "fox_auth_string": "ENCYPTED__xMCZ+WFxU+TJYaaR1P4OHK2ALYYnHqkLKhfDalRc7TQ=",
  "exp": 1740675959,
  "iss": "https://jwt.neodeex.net",
  "aud": "https://demo.webapi.neodeex.net"
}
```

### Authorized test

JWT 토큰을 발급 받았으므로 JWT 토큰을 포함하여 Fox Biz Service Web API 를 호출할 수 있습니다. `Authorization` 헤더에 발급받은 토큰을 포함하여 다시 호출해 봅니다.

```http
POST {{webapi_app_HostAddress}}/api/bizservice/execute
Accept: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cC ... 생략 ...
Content-Type: application/json

{
  "classId": "webapi_app.Biz.TestBizLogic",
  "methodId": "GetHello"
}
```

유효한 JWT 토큰이 포함되었고 JWT 토큰에서 NeoDEEX 토큰을 찾을 수 있으므로 Fox Biz Service 호출이 성공될 것이고 결과를 반환할 것입니다.

```json
{
  "success": true,
  "elapsedMilliseconds": 5,
  "result": {
    "$type": "string",
    "value": "Hello, NeoDEEX FoxBizService World! from tester   USER_ID=tester;DEPT=IT;"
  }
}
```

## Summary

Fox Biz/Data Service Web API 에서 사용하는 인증/권한 확인의 핵심은 `HttpContext` 객체에 `FoxUserInfoContext` 객체가 기록되어 있는가 입니다. FoxAuthentication 은 HTTP 헤더에서 NeoDEEX 토큰을 추출하여 `FoxUserInfoContext` 객체를 생성하고 `HttpContext` 객체에 기록합니다. FoxAuthorization 은 `HttpContext` 객체에 유효한 `FoxUserInfoContext` 가 존재하는 경우에 서비스 호출을 허용합니다.

따라서 `FoxUserInfoContext` 객체를 생성하고 `SetAuthenticated` 메서드를 호출할 수 있다면 JWT 인증이나 Goggle 인증 등 다양한 인증과 연동이 가능합니다.

* 이 예제처럼 JWT 토큰에 NeoDEEX 토큰을 포함시켜 토큰을 발행하고 인증시 JWT 토큰에서 NeoDEEX 토큰을 추출하여 FoxUserInfoContext 객체를 복원할 수 있습니다. 

* 다른 인증 방법에서 사용자 Id 정도만을 알아낼 수 있다면 인증 후 캐시, DB 액세스 등의 방법을 통해 `FoxUserInfoContext` 객체를 "생성"하여 `SetAuthenticated` 메서드를 호출할 수도 있습니다.

---
