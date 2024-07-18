using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

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
    /// 职位代码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }


    public Position(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null) : base(id)
    {
        Description = description;
        Status = StatusEnum.Enable;

        Name = Guard.Against.NullOrWhiteSpace
        (
            input: name,
            parameterName: nameof(name),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.PositionNameNull)
        );

        Code = Guard.Against.NullOrWhiteSpace
        (
            input: code,
            parameterName: nameof(code),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.PositionCodeNull)
        );
    }

    /// <summary>
    /// 更新职位名称
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Position UpdatePositionName([NotNull] string name)
    {
        Name = Guard.Against.NullOrWhiteSpace
        (
            input: name,
            parameterName: nameof(name),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.PositionNameNull)
        );

        return this;
    }

    /// <summary>
    /// 更新职位编码
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Position UpdatePositionCode([NotNull] string code)
    {
        Code = Guard.Against.NullOrWhiteSpace
        (
            input: code,
            parameterName: nameof(code),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.PositionCodeNull)
        );

        return this;
    }
}
