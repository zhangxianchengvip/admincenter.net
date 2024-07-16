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
    /// 机构编码
    /// </summary>
    public string Code { get; private set; }
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
        [NotNull] string code,
        Guid? superiorOrganizationId,
        string? description = null) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        Name = name;
        Code = code;
        SuperiorOrganizationId = superiorOrganizationId;
        Description = description;

    }

    /// <summary>
    /// 更新组织名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (Name == name) return this;

        Name = name;

        return this;
    }


    /// <summary>
    /// 更新组织编码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationCode(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));

        if (Code == code) return this;

        Code = code;

        return this;
    }


}
