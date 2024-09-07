using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webApi.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class VersionController : ControllerBase
{
    /// <summary>
    /// 1.0 Hello
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    public string Hello()
    {
        return "Hello world from Hello!";
    }
    
    /// <summary>
    /// 1.0 Hello3
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [Cola.Swagger.ActionName("Hello3")]
    public string Hello3()
    {
        return "Hello world from 1.0 Hello3!";
    }

    /// <summary>
    /// 1.1 hello
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.1")]
    [Cola.Swagger.ActionName("Hello")]
    public string Hello2()
    {
        return "Hello world from Hello2!";
    }
}