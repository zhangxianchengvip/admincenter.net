namespace AdminCenter.Domain.Common;
public interface IDomainEvent
{
    public Guid EventId { get; set; }

    public DateTimeOffset Created { get; set; }
}
