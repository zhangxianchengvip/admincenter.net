using AdminCenter.Domain.Common.Entities;

namespace AdminCenter.Domain;

/// <summary>
/// 用户组织
/// </summary>
public class UserOrganization : AuditableEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    public required Guid OrganizationId { get; set; }

    /// <summary>
    /// 附属组织
    /// </summary>
    public required bool IsSubsidiary { get; set; }

}
