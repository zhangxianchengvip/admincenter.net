namespace AdminCenter.Domain.Entities;
public class UserPosition : AuditableEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public required Guid PositionId { get; set; }
}
