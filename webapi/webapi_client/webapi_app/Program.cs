using NeoDEEX.ServiceModel.WebApi;

namespace webapi_app;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataServiceHelpPage(action);
        });

        app.MapPost("api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataService(action);
        });

        app.Run();
    }
}
