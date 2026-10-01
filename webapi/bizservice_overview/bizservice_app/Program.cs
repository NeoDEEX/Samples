using BizLib1;
using NeoDEEX.Data;
using NeoDEEX.Diagnostics;
using NeoDEEX.ServiceModel.Biz;
using NeoDEEX.ServiceModel.Services.Biz;
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

        // FoxBizService 직접 호출 예제.
        // 물론 이런 상황이라면 직접 BizLogic2 클래스의 GetProduct 메서드 호출이 더 효율적이다.
        app.MapGet("/api/product/{id}", (int id) =>
        {
            FoxBizService service = new();
            FoxBizRequest request = new("BizLib2.BizLogic2", "GetProduct");
            request["id"] = id;
            FoxBizResponse response = service.Execute(request);
            return response.Result;
        });

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

        FoxBizServiceDispatchOptions bizOptions = new()
        {
            UseTypeInfo = true
        };
        app.MapPost("/api/typed/bizservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchBizService(action, bizOptions);
        });

        app.Run();
    }
}
