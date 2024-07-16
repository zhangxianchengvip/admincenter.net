namespace AdminCenter.Domain;

public class UserPasswordUpdateEvent : DomainEvent
{
    public Guid UserId { get; set; }
}
