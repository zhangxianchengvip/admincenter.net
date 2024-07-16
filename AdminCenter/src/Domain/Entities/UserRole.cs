namespace AdminCenter.Domain;

/// <summary>
/// 用户角色
/// </summary>
public class UserRole : AuditableEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; private set; }

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
