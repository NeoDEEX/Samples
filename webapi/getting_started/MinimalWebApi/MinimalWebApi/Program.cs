using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.WebApi;
using NeoDEEX.Text.Json;
using System.Text.Json;

namespace MinimalWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.ConfigureBizService();

        // FoxBizService를 위한 about/test 페이지 지원
        app.MapGet("/api/bizservice/{action}", async (HttpRequest req, string? action) =>
        {
            return await req.DispatchBizServiceHelpPage(action);
        });
        // FoxBizService 를 위한 MapPost 호출
        // 주) DispatchOptions 객체를 매 호출마다 생성하지 않는 것이 성능상 유리합니다.
        FoxBizServiceDispatchOptions bizOptions = new()
        {
            UseTypeInfo = false,
            ReturnServerInfo = false,
        };
        app.MapPost("/api/bizservice/{action}", async (HttpRequest httpRequest, string? action) =>
        {
            return await httpRequest.DispatchBizService(action, bizOptions);
        });

        // FoxDataService를 위한 about/test 페이지 지원
        app.MapGet("/api/dataservice/{action}", async (HttpRequest req, string? action) =>
        {
            return await req.DispatchDataServiceHelpPage(action);
        });
        // FoxDataService 를 위한 MapPost 호출
        // 주) DispatchOptions 객체를 매 호출마다 생성하지 않는 것이 성능상 유리합니다.
        FoxDataServiceDispatchOptions dataOptions = new()
        {
            UseTypeInfo = false,
            ReturnServerInfo = false,
        };
        app.MapPost("/api/dataservice/{action}", async (string action, HttpRequest httpRequest) =>
        {
            return await httpRequest.DispatchDataService(action);
        });
        app.MapPost("/api/northwind/products", async (HttpRequest httpRequest) =>
        {
            return await httpRequest.DispatchDataService("executeDataSet", dataOptions);
        });

        // REST 스타일 Data Service
        JsonSerializerOptions jsonOptions = new();
        jsonOptions.Converters.Add(new FoxDataTableConverter() { UseTypeInfo = false });
        app.MapGet("/api/products/{id}", (string id, HttpRequest httpRequest) =>
        {
            FoxDataRequest request = new("Northwind:GetProducts");
            request["productid"] = id;

            FoxDataResponse response = httpRequest.DispatchDataRequest(service => service.ExecuteDataSet(request), dataOptions);
            return Results.Json(response.DataSet.Tables[0], jsonOptions);
        });

        app.Run();
    }
}
