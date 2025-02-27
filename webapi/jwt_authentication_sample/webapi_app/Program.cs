using NeoDEEX.ServiceModel.WebApi;

namespace webapi_app;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // NeoDEEX 의 WebApi 컨트롤러를 추가합니다.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service 비즈 모듈 초기화
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
