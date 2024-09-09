using Cola.Authen.Jwt;
using Cola.Swagger;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // https://learn.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencyinjection.mvcnewtonsoftjsonoptionsextensions.usemembercasing?view=aspnetcore-8.0
        //options.UseMemberCasing();
        options.UseCamelCasing(true);
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddColaSwagger(config);
builder.Services.AddColaJwt(config);

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