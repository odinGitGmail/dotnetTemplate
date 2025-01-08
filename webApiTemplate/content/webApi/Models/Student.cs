using Cola.Orm.EntityBase;
using SqlSugar;

namespace webApi.Models;

/// <summary>
/// Student
/// </summary>
[SugarTable("tb_Student")]
public class Student : IEntityBase<int>
{
    /// <summary>
    /// Id
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键且自增
    public int Id { get; set; }
    
    /// <summary>
    /// StudentName
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? StudentName { get; set; }
    
    /// <summary>
    /// Age
    /// </summary>
    public int Age { get; set; }
}