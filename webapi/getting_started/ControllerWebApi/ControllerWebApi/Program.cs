using NeoDEEX.ServiceModel.WebApi;

namespace ControllerWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Fox Biz/Data Service Web API 컨트롤러 추가
        builder.Services.AddControllers().AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service 비즈 모듈 초기화
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
