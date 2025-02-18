using SqlSugar;

namespace webApi.Models;
/// <summary>
/// Grade
/// </summary>
[SugarTable("tb_Grade")]
public class Grade : EntityBase<int>
{
    /// <summary>
    /// GradeName
    /// </summary>
    [SugarColumn(Length = 255)]
    public string? GradeName { get; set; }
}