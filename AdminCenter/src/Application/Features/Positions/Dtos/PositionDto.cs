using AdminCenter.Domain;

namespace AdminCenter.Application.Features.Positions.Dtos;
public class PositionDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 职位描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 职位代码
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }
}
