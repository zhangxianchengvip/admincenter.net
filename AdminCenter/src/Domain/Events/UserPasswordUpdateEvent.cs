namespace AdminCenter.Domain;

public class UserPasswordUpdateEvent : Event
{
    public Guid UserId { get; set; }
}
