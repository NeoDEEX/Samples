using NeoDEEX.ServiceModel.WebApi;

namespace SampleWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddFoxWebApiControllers();

        // Add services to the container.
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseFoxAuthentication();

        app.MapControllers();

        app.Run();
    }
}
