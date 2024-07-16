using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain;

/// <summary>
/// 职位
/// </summary>
public class Position : IAggregateRoot<Guid>
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
        Description = description;
        Status = StatusEnum.Enable;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    }

    /// <summary>
    /// 更新职位名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Position UpdatePositionName([NotNull] string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name)); ;

        return this;
    }
}
