using System.ComponentModel.DataAnnotations.Schema;
using AdminCenter.Domain.Common.Domain;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain.Common.Entities;

/// <summary>
/// 审计聚合根
/// </summary>
public abstract class AuditableAggregateRoot : IAuditableAggregateRoot
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

    /// <summary>
    /// 事件集合
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// 领域事件集合
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// 添加领域事件
    /// </summary>
    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    /// <summary>
    /// 移出事件
    /// </summary>
    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    /// <summary>
    /// 清除领域事件
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}

/// <summary>
/// 聚合根
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class AuditableAggregateRoot<TKey> : AuditableAggregateRoot, IAuditableAggregateRoot<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; init; } = default!;

    public AuditableAggregateRoot() { }

    public AuditableAggregateRoot(TKey id)
    {
        if (id == null)
        {
            throw new BusinessException("唯一标识不能为空");
        }

        Id = id;
    }
}
