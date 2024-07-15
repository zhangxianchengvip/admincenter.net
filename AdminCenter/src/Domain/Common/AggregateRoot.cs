namespace AdminCenter.Domain;

public abstract class AggregateRoot<T> : AuditableEntity<T>
{
    protected AggregateRoot(T id) : base(id)
    {

    }

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
