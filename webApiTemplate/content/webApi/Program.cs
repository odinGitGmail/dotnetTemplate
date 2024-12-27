using Cola.Console;
using Cola.Core;
using Cola.EF.EfInject;
using Cola.FilterExtensions;
using Cola.Models.Core.Models.ColaSqlsugar;
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
var colaConsole = builder.Services.BuildServiceProvider().GetService<IColaConsole>();
builder.Services.AddSingletonColaSqlSugar(config
    // null,
    // tableFilter:(new List<GlobalQueryFilter>()
    // {
    //     new GlobalQueryFilter()
    //     {
    //         ConfigId = "1",
    //         QueryFilter = (provider => provider.AddTableFilter<IStatus>(t => t.IsDelete == false))
    //     }
    // }),
    // aopOnLogExecutingModels:new List<AopOnLogExecutingModel>()
    // {
    //     new AopOnLogExecutingModel()
    //     {
    //         ConfigId   = "1",
    //         AopOnLogExecuting = ((sql, parameters) =>
    //         {
    //             colaConsole.WriteInfo($"sql is\n{sql}");
    //         })
    //     }
    // },
    // aopOnErrorModels:new List<AopOnErrorModel>()
    // {
    //     new AopOnErrorModel()
    //     {
    //         ConfigId = "1",
    //         AopOnError = (colaConsole.WriteException)
    //     }
    // }
);
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