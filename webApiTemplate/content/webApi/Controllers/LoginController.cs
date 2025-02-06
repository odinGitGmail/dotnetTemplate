using Cola.Authen;
using Cola.EF.Core.Interfaces;
using Cola.Models.Core.Models.ColaApiResult;
using Cola.Models.Core.Models.ColaAuthen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SqlSugar;
using webApi.Models;

namespace webApi.Controllers;


/// <summary>
/// LoginController
/// </summary>
/// <param name="authenToken">IAuthenToken</param>
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class LoginController(IAuthenToken authenToken,IUnitOfWork uow) : ControllerBase
{
    /// <summary>
    /// Jwt Token 的生成
    /// </summary>
    /// <returns>Token</returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ActionName("token")]
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
    /// student GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [HttpGet]
    [ActionName("GetById")]
    public IActionResult GetById(int id)
    {
        var repo = uow.GetRepository<Student,int>();
        var student = repo.GetById(id);
        return Ok(student);
    }
    
    
    
}