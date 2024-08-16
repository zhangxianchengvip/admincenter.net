using System;
using AdminCenter.Domain;
using AdminCenter.Domain.Enums;

namespace AdminCenter.Application.Features.Menus.Dtos;

public class MenuWithChildrenDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public MenuTypeEnum MenuType { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;

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
    /// 上级
    /// </summary>
    public Guid? SuperiorId { get; set; }

    /// <summary>
    /// 子级
    /// </summary>
    public List<MenuWithChildrenDto>? Children { get; set; }
}
