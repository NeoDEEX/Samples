using NeoDEEX.Data;
using NeoDEEX.ServiceModel.WebApi;

namespace foxwebapi_app;

public class Program
{
    public static void Main(string[] args)
    {
        // NeoDEEX 구성 설정에서 user-secrets 를 읽기 위한 설정
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();


        var app = builder.Build();

        // Configure the HTTP request pipeline.

        // Linux 환경에서 HTTPS 리디렉션이 인증서 문제를 일으킬 수 있으므로
        // 테스트 환경은 그냥 HTTP로 사용하도록 주석 처리
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataServiceHelpPage(action);
        });

        app.MapPost("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataService(action);
        });

        app.Run();
    }
}
