using NeoDEEX.Data;

// user secrets 에서 연결 문자열을 읽어 오도록 구성 설정 조정.
var config = new ConfigurationBuilder()
    .AddUserSecrets("SimpleIsBest").Build();
FoxDatabaseConfig.ExternalConfiguration = config;

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
