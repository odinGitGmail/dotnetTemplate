using System.ComponentModel.DataAnnotations;
using Cola.EF.Core.Interfaces;
using Cola.EF.SqlSugar;
using SqlSugar;

namespace webApi.Models;

/// <summary>
/// Student
/// </summary>
[SugarTable("tb_Student")]
public class Student : EntityBase<int>
{
    /// <summary>
    /// StudentName
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? StudentName { get; set; }
    
    /// <summary>
    /// Age
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// GradeId
    /// </summary>
    public int? GradeId { get; set; }
}