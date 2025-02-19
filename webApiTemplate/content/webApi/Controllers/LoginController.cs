using System.Linq.Expressions;
using Cola.Authen;
using Cola.EF.Core.Interfaces;
using Cola.Models.Core.Models.ColaApiResult;
using Cola.Models.Core.Models.ColaAuthen;
using Cola.Models.Core.Models.ColaEF;
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
        return ApiResult<DateTime>.Success(
            DateTime.Now,
            authenToken.GenerateToken(new Dictionary<string, string>()
            {
                { JwtRegisteredClaimNames.Name, "odinsam" }
            }));
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
    public ApiResult<Student> GetById(int id)
    {
        try
        {
            uow.BeginTransaction();
            var student = uow.GetRepository<Student, int>();
            var stu = student.GetSingleOrDefault(1);
            uow.CommitTransaction();
            return ApiResult<Student>.Success(stu);
        }
        catch (Exception e)
        {
            uow.RollbackTransaction();
            throw new Exception("error");
        }
    }
    
    /// <summary>
    /// student GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [HttpGet]
    [ActionName("GetStudenInfo")]
    public ApiResult<List<StudentGrade>> GetStudenInfo(int id)
    {
        try
        {
            uow.BeginTransaction();
            var student = uow.GetRepository<Student, int>();
            var grade = uow.GetRepository<Grade, int>();
            var joinExpression = uow.LeftJoin<Student, Grade>((s, g) => s.GradeId == g.Id);
            var whereExpression = uow.WhereExpression<Student>(s => s.StudentName.Contains("odin"));
            var orderExpressions = new List<OrderExpression<Student>>
            {
                new OrderExpression<Student>(){ Order = s=>s.Age,OrderType = OrderByType.Asc},
            };
            var whereIfExpressions = new List<WhereIfExpression<Student>>
            {
                new WhereIfExpression<Student>() { IsWhere = true, WhereIf = s => s.Age > 23 }
            };
            var selectExpression = uow.SelectExpression<Student,StudentGrade>(s => new StudentGrade
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Age = s.Age,
                GradeName = SqlFunc.Subqueryable<Grade>().Where(g=> g.Id== s.Id).Select(g=>g.GradeName)
            });
            var stuGrade = student.Query(
                selectExpression, 
                joinExpression, 
                [whereExpression],
                whereIfExpressions,orderExpressions);
            
            uow.CommitTransaction();
            return ApiResult<List<StudentGrade>>.Success(stuGrade);
        }
        catch (Exception e)
        {
            uow.RollbackTransaction();
            throw new Exception("error");
        }
    }
    
    /// <summary>
    /// student GetById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [HttpGet]
    [ActionName("GetStudenPages")]
    public ApiResult<PageQueryResponse<StudentGrade>> GetStudenPages(int pageNumber)
    {
        try
        {
            uow.BeginTransaction();
            var student = uow.GetRepository<Student, int>();
            var grade = uow.GetRepository<Grade, int>();
            var joinExpression = uow.LeftJoin<Student, Grade>((s, g) => s.GradeId == g.Id);
            var whereExpression = uow.WhereExpression<Student>(s => s.StudentName.Contains("odin"));
            var orderExpressions = new List<OrderExpression<Student>>
            {
                new OrderExpression<Student>(){ Order = s=>s.Age,OrderType = OrderByType.Asc},
            };
            var whereIfExpressions = new List<WhereIfExpression<Student>>
            {
                new WhereIfExpression<Student>() { IsWhere = true, WhereIf = s => s.Age > 23 }
            };

            var primaryKeyExpression = uow.PrimaryKeyExpression<Student, int>(s => s.Id);
            
            var selectExpression = uow.SelectExpression<Student,StudentGrade>(s => new StudentGrade
            {
                Id = s.Id,
                StudentName = s.StudentName ?? string.Empty,
                Age = s.Age,
                GradeName = SqlFunc.Subqueryable<Grade>().Where(g=> g.Id== s.Id).Select(g=>g.GradeName)
            });
            var stuGrade = student.QueryPageing(
                new PageQueryResponse<StudentGrade>(){PageNumber = pageNumber},
                selectExpression, 
                primaryKeyExpression,
                joinExpression,
                [whereExpression],
                whereIfExpressions,
                orderExpressions);
            
            uow.CommitTransaction();
            return ApiResult<PageQueryResponse<StudentGrade>>.Success(stuGrade);
        }
        catch (Exception e)
        {
            uow.RollbackTransaction();
            throw new Exception("error");
        }
    }
}