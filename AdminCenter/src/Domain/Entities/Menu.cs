using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Entities;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Enums;

namespace AdminCenter.Domain.Entities;

/// <summary>
/// 菜单
/// </summary>
public class Menu : AggregateRoot<Guid>
{
    /// <summary>
    /// 类型
    /// </summary>
    public MenuTypeEnum MenuType { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路由
    /// </summary>
    public string? Route { get; set; }

    /// <summary>
    /// 是否外链
    /// </summary>
    public bool IsLink { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 角色菜单
    /// </summary>
    public ICollection<RoleMenu> RoleMenu { get; set; } = [];
    /// <summary>
    /// 上级Id
    /// </summary>
    public Guid? SuperiorId { get; set; }

    public Menu(
        [NotNull] Guid id,
        MenuTypeEnum menuType,
        [NotNull] string name,
        string? route,
        bool isLink,
        Guid? superiorId) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.MenuNameNull);
        }

        Route = route;
        IsLink = isLink;
        MenuType = menuType;
        Status = StatusEnum.Enable;
        SuperiorId = superiorId;
        Name = name;
    }

    /// <summary>
    /// 更新菜单名称
    /// </summary>
    public Menu UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessException(ExceptionMessage.MenuNameNull);
        }

        Name = name;

        return this;
    }
}
