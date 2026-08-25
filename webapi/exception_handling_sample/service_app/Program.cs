using NeoDEEX.Data;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.Services.Data;
using NeoDEEX.ServiceModel.WebApi;
using NeoDEEX.Text.Json;
using System.Data;
using System.Text.Json;

namespace service_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NeoDEEX 구성 설정에서 user-secrets 를 읽기 위한 설정
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            FoxDatabaseConfig.ExternalConfiguration = config;

            // 데이터베이스 셋업 수행
            Console.Write("Setting up database...");
            string script = File.ReadAllText("./db/setup_script.sql");
            FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
            dbAccess.ExecuteSqlNonQuery(script);
            Console.WriteLine("Done");

            // WebApplication 빌더 생성 및 셋업
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // 서비스 측에서 FoxDataServiceException 을 사용하여 예외를 처리하는 예제.
            // 항상 예외를 유발하는 쿼리("sample.error_query_id")를 사용하여 FoxDataServiceException 을 발생시킴.
            app.MapGet("/api/product/{id}", (string id) =>
            {
                FoxDataService service = new();
                FoxDataRequest request = new("sample.error_query_id");
                request.Parameters.Add("product_id", id);
                try
                {
                    FoxDataResponse response = service.ExecuteDataSet(request);
                    if (response.DataSet == null || response.DataSet.Tables.Count == 0
                        || response.DataSet.Tables[0].Rows.Count == 0)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(response);
                }
                catch(FoxDataServiceException ex)
                {
                    string errorType = ex.InnerException != null ? ex.InnerException.GetType().Name : ex.GetType().Name;
                    return Results.Problem(detail: ex.Message, type: errorType);
                }
            });

            // Fox Data Service Help Page(html) 엔드 포인트
            app.MapGet("/api/dataservice/{action}", (string? action, HttpRequest request) =>
            {
                return request.DispatchDataServiceHelpPage(action);
            });
            // Fox Data Service Web API 엔드 포인트
            app.MapPost("/api/dataservice/{action}", (string? action, HttpRequest request) =>
            {
                return request.DispatchDataService(action);
            });

            app.Run();
        }
    }
}
