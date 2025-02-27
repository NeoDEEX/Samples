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

        // JWT Bearer ������ �߰��մϴ�.
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

        // NeoDEEX �� WebApi ��Ʈ�ѷ��� �߰��մϴ�.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service ���� ��� �ʱ�ȭ
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        // JWT Bearer ������ �����ϵ��� �մϴ�.
        app.UseAuthentication();

        // Fox Authentication �� ������� �ʰ� JWT ��ū���κ��� ������ �����մϴ�.
        //app.UseFoxAuthentication();
        // JWT ��ū���� NeoDEEX ��ū�� �����ϱ� ���� ���� ���ڿ� �����ϱ� ���� �̵���� �߰�
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

        // asp.net �� ���� Ȯ���� �ʿ��� ��� ȣ���մϴ�.
        // Fox Web API �� �� ȣ���� �ʿ����� �ʽ��ϴ�.
        //app.UseAuthorization();

        app.MapControllers();

        // �߰����� API�� ȣ���ϱ� ���� �α����� ȣ���Ͽ� JWT ��ū�� �߱��մϴ�.
        app.MapPost("/api/login", async (HttpContext context, RequestUserCredential credential) =>
        {
            // ������ �����̹Ƿ� ����� ������ �ϵ��ڵ����� ó���մϴ�.
            // ���� �ۿ����� DB, ���͸� ���� ����Ͽ� ����ڸ� �����ؾ� �մϴ�.
            if ((credential.UserId == "admin" && credential.Password == "admin")
                || (credential.UserId == "tester" && credential.Password == "test"))
            {
                // FoxUserInfoContext�� �����ϰ� NeoDEEX ��ū���� ��ȯ�մϴ�.
                FoxUserInfoContext ctx = new(credential.UserId);
                ctx["DEPT"] = "IT";
                string authString = FoxUserInfoContext.VersionIndependentSerialize(ctx);
                // JWT ��ū�� �����ϰ� ��ȯ�մϴ�.
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
