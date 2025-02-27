using NeoDEEX.ServiceModel.WebApi;

namespace webapi_app;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // NeoDEEX �� WebApi ��Ʈ�ѷ��� �߰��մϴ�.
        builder.Services.AddControllers()
            .AddFoxWebApiControllers();

        var app = builder.Build();

        // Fox Biz Service ���� ��� �ʱ�ȭ
        app.ConfigureBizService();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
