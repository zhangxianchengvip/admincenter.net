using MediatR;

namespace AdminCenter.Domain.Common;

public class Event : IEvent, INotification
{
    public Guid EventId { get; set; }
    public DateTimeOffset Created { get; set; }
}
