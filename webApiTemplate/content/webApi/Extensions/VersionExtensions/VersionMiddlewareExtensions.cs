namespace webApi.Extensions.VersionExtensions;

public static class VersionSwaggerMiddlewareExtensions
{
    public static IApplicationBuilder UseVersionSwagger(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VersionSwaggerMiddleware>();
    }
}