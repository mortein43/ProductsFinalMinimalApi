namespace ProductsFinalMinimalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddRouting();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseHttpsRedirection();


        app.UseRouting();


        app.Map("/", async (context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "html/login.html"));
        });

        app.Map("/index", async (context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "html/index.html"));
        });
        app.Map("/api/login", async (context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "html/index.html"));
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/index", async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/index.html");
            });
        });

        app.Run();
    }
}