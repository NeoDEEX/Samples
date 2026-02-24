using commlib;

namespace legacy_webapi;

public class Program
{
    public static void Main(string[] args)
    {
        // 테스트 환경 구성
        Utils.SetupTest<Program>();

        //
        // ASP.NET Web API 초기화 코드
        //
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
