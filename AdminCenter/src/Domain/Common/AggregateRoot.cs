using AdminCenter.Domain.Common;

namespace AdminCenter.Domain;


public abstract class AggregateRoot : IAuditableEntity
{
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    private readonly List<Event> _domainEvents = new();

    public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(Event domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(Event domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
public abstract class IAggregateRoot<TKey> : AggregateRoot, IAuditableEntity<TKey>
{
    public TKey Id { get; init; }
    protected IAggregateRoot(TKey id)
    {
        Id = id;
    }
}
