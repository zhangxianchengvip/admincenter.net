using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Entities;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Entities;
namespace AdminCenter.Domain;

public class Role : AggregateRoot<Guid>
{

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = default!;

    /// <summary>
    /// 角色显示名称
    /// </summary>
    public string ShowName { get; private set; } = default!;

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];

    /// <summary>
    /// 角色菜单
    /// </summary>
    public ICollection<RoleMenu> RoleMenu { get; set; } = [];

    public Role(
        [NotNull] Guid id,
        [NotNull] string roleName,
        [NotNull] string showName,
        int order,
        string? description = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            throw new BusinessException(ExceptionMessage.RoleNameNull);
        }

        if (string.IsNullOrWhiteSpace(showName))
        {
            throw new BusinessException(ExceptionMessage.RoleShowNameNull);
        }

        RoleName = roleName;
        ShowName = showName;
        Order = order;
        Description = description;
        Status = StatusEnum.Enable;
    }

    /// <summary>
    /// 更新角色名称
    /// </summary>
    public Role UpdateRoleName([NotNull] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            throw new BusinessException(ExceptionMessage.RoleNameNull);
        }
        RoleName = roleName;
        return this;
    }
    /// <summary>
    /// 更新角色显示名称
    /// </summary>
    public Role UpdateShowName([NotNull] string showName)
    {
        if (string.IsNullOrWhiteSpace(showName))
        {
            throw new BusinessException(ExceptionMessage.RoleShowNameNull);
        }
        ShowName = showName;
        return this;
    }

}
