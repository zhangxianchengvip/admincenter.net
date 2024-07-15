using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain;

/// <summary>
/// 职位
/// </summary>
public class Position : AggregateRoot<Guid>
{

    /// <summary>
    /// 职位名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }


    public Position(
        [NotNull] Guid id,
        [NotNull] string name,
        string? description = null) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Name = name;
        Description = description;
        Status = StatusEnum.Enable;
    }

    /// <summary>
    /// 更新职位名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Position UpdatePositionName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Name = name;

        return this;
    }
}
