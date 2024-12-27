using Cola.Authen;
using Cola.EF.BaseRepository;
using Cola.Models.Core.Models.ColaApiResult;
using Cola.Models.Core.Models.ColaAuthen;
using Cola.Utils;
using Cola.Utils.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using webApi.Models;

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
    
    /// <summary>
    /// GetString
    /// </summary>
    /// <returns>Token</returns>
    [AllowAnonymous]
    [HttpGet]
    [ApiVersion("1.0")]
    public ApiResult<string> GetString()
    {
        // throw new ColaException(enumException: EnumException.SyS000006);
        return new ApiResult<string>()
        {
            Data = "create Table"
        };
    }
}