using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Entities;
using AdminCenter.Domain.Constants;

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
    public Guid? SuperiorId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 用户组织
    /// </summary>
    public ICollection<UserOrganization> UserOrganizations { get; set; } = [];

    public Organization(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        Guid? superiorId,
        string? description = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.OrganizationNameNull);
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new BusinessException(ExceptionMessage.OrganizationCodeNull);
        }

        Name = name;
        Code = code;
        Description = description;
        SuperiorId = superiorId;
    }

    /// <summary>
    /// 更新组织名称
    /// </summary>
    public Organization UpdateOrganizationName([NotNull] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.OrganizationNameNull);
        }

        Name = name;

        return this;
    }


    /// <summary>
    /// 更新组织编码
    /// </summary>
    public Organization UpdateOrganizationCode([NotNull] string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new BusinessException(ExceptionMessage.OrganizationCodeNull);
        }

        Code = code;

        return this;
    }
}
