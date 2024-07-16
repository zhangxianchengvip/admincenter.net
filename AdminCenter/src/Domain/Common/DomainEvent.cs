using MediatR;

namespace AdminCenter.Domain.Common;

public class DomainEvent : IDomainEvent, INotification
{
    /// <summary>
    /// 事件Id
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset Created { get; set; }
}
