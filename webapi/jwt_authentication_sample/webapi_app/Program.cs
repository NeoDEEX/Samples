using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NeoDEEX.Security;
using NeoDEEX.ServiceModel.WebApi;

namespace webapi_app;

public class Program
{
    private static readonly string WebApiAudience = "https://demo.webapi.neodeex.net";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

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

        // NeoDEEX 의 WebApi 컨트롤러를 추가합니다.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service 비즈 모듈 초기화
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

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
                app.Logger.LogInformation("CustomAuth User:{user}", userId);
                if (string.IsNullOrEmpty(authString) == false)
                {
                    FoxUserInfoContext ctx = FoxUserInfoContext.VersionIndependentDeserialize(authString);
                    context.SetAuthenticated(ctx);
                }
            }
            await next.Invoke();
        });

        // asp.net 의 권한 확인이 필요한 경우 호출합니다.
        // Fox Web API 는 이 호출이 필요하지 않습니다.
        //app.UseAuthorization();

        app.MapControllers();

        // 추가적인 API를 호출하기 위해 로그인을 호출하여 JWT 토큰을 발급합니다.
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

        app.Run();
    }
}
