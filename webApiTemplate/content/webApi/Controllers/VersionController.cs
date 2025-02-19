using Cola.Models.Core.Models.ColaApiResult;
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
    public ApiResult<string> Hello()
    {
        return ApiResult<string>.Success("Hello world from Hello!");
    }
}

