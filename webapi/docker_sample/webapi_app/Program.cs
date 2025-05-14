using NeoDEEX.Configuration;
using NeoDEEX.Data;
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.ServiceModel.WebApi;

namespace webapi_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 구성 설정 파일 경로 지정
            FoxConfigurationManager.ConfigurationFileName = "./config/neodeex.config.json";
            // user-secrets 설정 (존재하는 경우에만)
            string? userSecretsId = FoxConfigurationManager.AppSettings["userSecretsId"];
            if (userSecretsId != null)
            {
                ConfigurationBuilder configBuilder = new();
                configBuilder.AddUserSecrets(userSecretsId);
                FoxDatabaseConfig.ExternalConfiguration = configBuilder.Build();
            }
            // FoxBizService 모듈 로드
            FoxBizServiceConfig.Configure();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseAuthorization();

            // FoxDataService 를 위한 MapPost 호출
            app.MapPost("/api/dataservice/{action}", async (string action, HttpRequest httpRequest) =>
            {
                return await httpRequest.DispatchDataService(action);
            });

            // FoxDataService 를 위한 MapPost 호출
            app.MapPost("/api/bizservice/{action}", async (string action, HttpRequest httpRequest) =>
            {
                return await httpRequest.DispatchBizService(action);
            });

            app.Run();
        }
    }
}
