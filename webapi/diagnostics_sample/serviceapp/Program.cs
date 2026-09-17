using NeoDEEX.Data;
using NeoDEEX.ServiceModel.WebApi;

namespace serviceapp;

public class Program
{
    public static void Main(string[] args)
    {
        // NeoDEEX 구성 설정에서 user-secrets 를 읽기 위한 설정
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        // 다음 정적 속성 혹은 구성 설정 "webapiServer:returnServerInfo" 속성이
        // true 가 아닌 경우 서버에 관련된 정보는 클라이언트에게 반환되지 않는다.
        // (디폴트 값은 false)
        FoxWebApiServerConfig.ReturnServerInfo = false;

        // WebApplication 빌더 생성 및 셋업
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.UseAuthorization();

        // Fox Data Service Help Page(html) 엔드 포인트
        app.MapGet("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataServiceHelpPage(action);
        });
        // Fox Data Service Web API 엔드 포인트
        app.MapPost("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataService(action);
        });

        app.Run();
    }
}
