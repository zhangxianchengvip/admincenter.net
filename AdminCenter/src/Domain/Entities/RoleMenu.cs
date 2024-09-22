using AdminCenter.Domain.Common.Entities;

namespace AdminCenter.Domain.Entities;
public class RoleMenu : AuditableEntity
{
    /// <summary>
    /// 菜单Id
    /// </summary>
    public required Guid MenuId { get; set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public required Guid RoleId { get; set; }
}
