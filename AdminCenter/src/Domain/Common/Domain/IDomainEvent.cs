namespace AdminCenter.Domain.Common.Domain;
public interface IDomainEvent
{
    public Guid EventId { get; set; }

    public DateTimeOffset Created { get; set; }
}
