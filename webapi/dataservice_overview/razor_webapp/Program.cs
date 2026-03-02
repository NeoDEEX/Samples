using NeoDEEX.Data;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.ServiceModel.WebApi;

namespace razor_webapp;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        app.MapGet("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataServiceHelpPage(action);
        });

        app.MapPost("/api/dataservice/{action}", (string? action, HttpRequest request) =>
        {
            return request.DispatchDataService(action);
        });

        app.Run();
    }
}
