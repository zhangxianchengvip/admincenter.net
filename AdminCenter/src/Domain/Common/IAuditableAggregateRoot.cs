public interface IAuditableAggregateRoot : IAuditableEntity, IAggregateRoot
{
}

public interface IAuditableAggregateRoot<TKey> : IAuditableAggregateRoot, IAuditableEntity<TKey>, IAggregateRoot<TKey>
{
}