using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Entities;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Entities;

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
    public string Code { get; private set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 用户职位
    /// </summary>
    public ICollection<UserPosition> UserPositions { get; private set; } = [];


    public Position(
        [NotNull] Guid id,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null) : base(id)
    {

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.PositionNameNull);
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new BusinessException(ExceptionMessage.PositionCodeNull);
        }

        Description = description;
        Status = StatusEnum.Enable;
        Name = name;
        Code = code;
    }

    /// <summary>
    /// 更新职位名称
    /// </summary>
    public Position UpdatePositionName([NotNull] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.PositionNameNull);
        }

        Name = name;

        return this;
    }

    /// <summary>
    /// 更新职位编码
    /// </summary>
    public Position UpdatePositionCode([NotNull] string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new BusinessException(ExceptionMessage.PositionCodeNull);
        }

        Code = code;

        return this;
    }
}
