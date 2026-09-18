using BizLib1;
using NeoDEEX.Data;
using NeoDEEX.Diagnostics;
using NeoDEEX.ServiceModel.WebApi;

namespace bizservice_app;

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

        // Fox Biz Service 구성
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.UseAuthorization();

        // Fox Biz Service Help Page(html) 엔드 포인트
        app.MapGet("/api/bizservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchBizServiceHelpPage(action);
        });
        // Fox Biz Service Web API 엔드 포인트
        app.MapPost("/api/bizservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchBizService(action);
        });

        app.Run();
    }
}
