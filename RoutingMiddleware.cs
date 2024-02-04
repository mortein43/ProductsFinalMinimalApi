namespace ProductsFinalMinimalApi;

public static class RoutingMiddlewareExtensions
{
    public static IApplicationBuilder UseRoutingMiddleware(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            var path = context.Request.Path.Value?.ToLower();

            switch (path)
            {
                case "/":
                    await HandleProductsRequest(context);
                    break;
                // Додайте інші маршрути за необхідності
                case "/index":
                    await HandleProductsRequest(context);
                    break;
                case "/login":
                    await HandleProductsRequest(context);
                    break;

                default:
                    await next();
                    break;
            }
        });
    }

    private static async Task HandleProductsRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Products Page");
    }
}
