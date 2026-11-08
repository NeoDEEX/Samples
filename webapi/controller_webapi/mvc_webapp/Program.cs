using NeoDEEX.Data;
using NeoDEEX.ServiceModel.WebApi;

namespace mvc_webapp;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers()
            // Fox Web API Controller 등록
            .AddFoxWebApiControllers();
        var app = builder.Build();
        // Fox Biz Service 초기화
        app.ConfigureBizService();
        app.UseFoxAuthentication();
        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
