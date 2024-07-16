using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Ardalis.GuardClauses;

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
        Code = code;
        Description = description;
        SuperiorOrganizationId = superiorOrganizationId;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

    }

    /// <summary>
    /// 更新组织名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationName([NotNull] string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

        return this;
    }


    /// <summary>
    /// 更新组织编码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationCode([NotNull] string code)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code));

        return this;
    }


}
