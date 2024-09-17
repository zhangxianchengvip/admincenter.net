using System.ComponentModel.DataAnnotations.Schema;

public interface IAggregateRoot : IEntity
{


    /// <summary>
    /// 添加领域事件
    /// </summary>
    public void AddDomainEvent(DomainEvent domainEvent);

    /// <summary>
    /// 移出事件
    /// </summary>
    public void RemoveDomainEvent(DomainEvent domainEvent);

    /// <summary>
    /// 清除领域事件
    /// </summary>
    public void ClearDomainEvents();
}

public interface IAggregateRoot<TKey> : IAggregateRoot, IEntity<TKey>
{

}