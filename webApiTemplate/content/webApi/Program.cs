using Cola.Core;
using Cola.FilterExtensions;
using Cola.Swagger;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddColaCore(config);
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ColaActionFilter));
    })
    .AddNewtonsoftJson(options =>
    {
        options.UseCamelCasing(true);
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddColaSwaggerAndJwt(config);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseColaSwagger(builder,config);
}

app.UseStaticFiles();
// 使用cookie
app.UseCookiePolicy();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseRouting();
//开启认证
app.UseAuthentication();
//授权中间件
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});
app.Run();