using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain;

/// <summary>
/// 组织
/// </summary>
public class Organization : AggregateRoot<Guid>
{
    /// <summary>
    /// 组织名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 上级组织
    /// </summary>
    public Guid? SuperiorOrganizationId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }
    public Organization(
        [NotNull] Guid id,
        [NotNull] string name,
        Guid? superiorOrganizationId,
        string? description = null) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Name = name;
        SuperiorOrganizationId = superiorOrganizationId;
        Description = description;

    }
}
