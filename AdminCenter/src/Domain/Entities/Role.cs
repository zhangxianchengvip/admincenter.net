using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain;

public class Role : IAggregateRoot<Guid>
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

    public Role(
        [NotNull] Guid id,
        [NotNull] string roleName,
        string? description = null) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(roleName, nameof(roleName));

        Name = roleName;
        Description = description;
        Status = StatusEnum.Enable;
    }
}
