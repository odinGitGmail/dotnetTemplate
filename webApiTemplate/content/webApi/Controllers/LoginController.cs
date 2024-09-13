using System.Runtime.InteropServices.JavaScript;
using Cola.Authen;
using Cola.Models.Core.Models;
using Cola.Models.Core.Models.ColaApiResult;
using Cola.Models.Core.Models.ColaAuthen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace webApi.Controllers;


/// <summary>
/// LoginController
/// </summary>
/// <param name="authenToken">IAuthenToken</param>
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class LoginController(IAuthenToken authenToken) : ControllerBase
{
    /// <summary>
    /// Jwt Token 的生成
    /// </summary>
    /// <returns>Token</returns>
    [HttpGet]
    [ApiVersion("1.0")]
    public ApiResult<DateTime> GetToken()
    {
        return new ApiResult<DateTime>()
        {
            Data = DateTime.Now,
            Token = new TokenModel()
            {
                AccessToken = new AccessTokenModel()
                {
                    TokenStr = authenToken.GenerateToekn(new Dictionary<string, string>()
                    {
                        {JwtRegisteredClaimNames.Name,"odinsam"}
                    })
                }
            }
        };
    }
}