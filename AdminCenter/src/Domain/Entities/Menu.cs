using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Enums;
using Ardalis.GuardClauses;

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
    /// 上级Id
    /// </summary>
    public Guid? SuperiorMenuId { get; set; }

    public Menu(
        [NotNull] Guid id,
        MenuTypeEnum menuType,
        [NotNull] string name,
        string? route,
        bool isLink,
        Guid? superiorMenuId) : base(id)
    {
        Route = route;
        IsLink = isLink;
        MenuType = menuType;
        Status = StatusEnum.Enable;
        SuperiorMenuId = superiorMenuId;
        Name = Guard.Against.NullOrWhiteSpace
       (
           input: name,
           parameterName: nameof(name),
           exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationNameNull)
       );
    }

    /// <summary>
    /// 更新菜单名称
    /// </summary>
    public Menu UpdateName(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace
        (
           input: name,
           parameterName: nameof(name),
           exceptionCreator: () => new BusinessException(ExceptionMessage.OrganizationNameNull)
        );

        return this;
    }
}
