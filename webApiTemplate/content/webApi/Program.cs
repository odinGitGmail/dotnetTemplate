using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using webApi.Extensions.VersionExtensions;
using ActionNameAttribute = webApi.Extensions.VersionExtensions.ActionNameAttribute;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<ReflectionCache>(new ReflectionCache());
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = false;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var reflectionCache = builder.Services.BuildServiceProvider().GetService<ReflectionCache>();
    // swagger 默认自动替换版本号
    c.OperationFilter<RemoveVersionFromParameter>();
    c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
    var swaggerTitle = config.GetSection("Swagger:VersionTitle").Get<string>();
    foreach (var version in reflectionCache.AllApiVersions)
    {
        c.SwaggerDoc($"v{version}", new OpenApiInfo() { Title = $"{swaggerTitle}", Version = $"v{version}" });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var reflectionCache = builder.Services.BuildServiceProvider().GetService<ReflectionCache>();
        var swaggerTitle = config.GetSection("Swagger:VersionTitle").Get<string>();
        foreach (var version in reflectionCache.AllApiVersions)
        {
            c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"{swaggerTitle} V{version}");
        }
    });
    app.UseVersionSwagger();
}

app.UseStaticFiles();
// 使用cookie
app.UseCookiePolicy();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});
app.Run();