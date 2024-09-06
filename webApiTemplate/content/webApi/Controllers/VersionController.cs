using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class VersionController : ControllerBase
{
    // route: /api/v1.0/WeatherForecast/Hello
    [HttpGet]
    [ApiVersion("1.0")]
    public string Hello()
    {
        return "Hello world from Hello!";
    }

// route: /api/v1.1/WeatherForecast/Hello2
    [HttpGet]
    [ApiVersion("1.1")]
    [ActionName("Hello")]
    [Authorize]
    public string Hello2()
    {
        return "Hello world from Hello2!";
    }
}