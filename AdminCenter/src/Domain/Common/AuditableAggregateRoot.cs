namespace AdminCenter.Domain;

public class AuditableAggregateRoot<T> : AuditableEntity<T>
{
    public AuditableAggregateRoot(T id) : base(id)
    {
    }
}
