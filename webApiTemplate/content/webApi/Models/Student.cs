using SqlSugar;

namespace webApi.Models;

[SugarTable("Student")]
public class Student
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键且自增
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}