using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Constants;
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
        Description = description;
        SuperiorOrganizationId = superiorOrganizationId;

        Code = Guard.Against.NullOrWhiteSpace
        (
            input: code,
            parameterName: nameof(code),
            exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationCodeNull)
        );

        Name = Guard.Against.NullOrWhiteSpace
        (
            input: name,
            parameterName: nameof(name),
            exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationNameNull)
        );

    }

    /// <summary>
    /// 更新组织名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationName([NotNull] string name)
    {
        Name = Guard.Against.NullOrWhiteSpace
        (
            input: name,
            parameterName: nameof(name),
            exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationNameNull)
        );

        return this;
    }


    /// <summary>
    /// 更新组织编码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Organization UpdateOrganizationCode([NotNull] string code)
    {
        Code = Guard.Against.NullOrWhiteSpace
        (
            input: code,
            parameterName: nameof(code),
            exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationCodeNull)
        );

        return this;
    }
}
