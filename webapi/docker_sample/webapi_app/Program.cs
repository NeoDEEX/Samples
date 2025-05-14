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
            // ���� ���� ���� ��� ����
            FoxConfigurationManager.ConfigurationFileName = "./config/neodeex.config.json";
            // user-secrets ���� (�����ϴ� ��쿡��)
            string? userSecretsId = FoxConfigurationManager.AppSettings["userSecretsId"];
            if (userSecretsId != null)
            {
                ConfigurationBuilder configBuilder = new();
                configBuilder.AddUserSecrets(userSecretsId);
                FoxDatabaseConfig.ExternalConfiguration = configBuilder.Build();
            }
            // FoxBizService ��� �ε�
            FoxBizServiceConfig.Configure();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseAuthorization();

            // FoxDataService �� ���� MapPost ȣ��
            app.MapPost("/api/dataservice/{action}", async (string action, HttpRequest httpRequest) =>
            {
                return await httpRequest.DispatchDataService(action);
            });

            // FoxDataService �� ���� MapPost ȣ��
            app.MapPost("/api/bizservice/{action}", async (string action, HttpRequest httpRequest) =>
            {
                return await httpRequest.DispatchBizService(action);
            });

            app.Run();
        }
    }
}
