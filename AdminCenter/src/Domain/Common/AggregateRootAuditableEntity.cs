using System.ComponentModel.DataAnnotations.Schema;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain;

/// <summary>
/// 聚合根
/// </summary>
public abstract class AggregateRootAuditableEntity : AggregateRoot, IAggregateRoot, IAuditableEntity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTimeOffset LastModified { get; set; }

    /// <summary>
    /// 最后修改人
    /// </summary>
    public string? LastModifiedBy { get; set; }
}

/// <summary>
/// 聚合根
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class AggregateRootAuditableEntity<TKey> : AggregateRootAuditableEntity, IAuditableEntity<TKey>, IAggregateRoot<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; init; } = default!;

    public AggregateRootAuditableEntity() { }

    public AggregateRootAuditableEntity(TKey id)
    {
        if (id == null)
        {
            throw new BusinessException("唯一标识不能为空");
        }

        Id = id;
    }
}
