using commlib;
using NeoDEEX.ServiceModel.WebApi;

namespace neodeex_webapi;

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

        // MVC 컨트롤러와 FoxWebApi 컨트롤러를 등록.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service 모듈 구성
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
