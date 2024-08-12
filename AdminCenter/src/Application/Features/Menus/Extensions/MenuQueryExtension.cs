using System;
using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Domain.Entities;

namespace AdminCenter.Application.Features.Menus.Extensions;

public static class MenuQueryExtension
{
    public static List<MenuWithChildrenDto> BuildMenuTree(this List<Menu> menus)
    {
        var menuDtos = new List<MenuWithChildrenDto>();
        var menuDictionary = new Dictionary<Guid, MenuWithChildrenDto>();

        // 将所有菜单转换为 DTO 并填充到字典中以便快速查找
        foreach (var menu in menus)
        {
            var menuDto = menus.Adapt<MenuWithChildrenDto>();
            menuDtos.Add(menuDto);
            menuDictionary[menu.Id] = menuDto;
        }

        // 遍历 DTO 列表，为每个菜单设置子菜单
        foreach (var menuDto in menuDtos)
        {
            if (menuDto.SuperiorId.HasValue && menuDictionary.TryGetValue(menuDto.SuperiorId.Value, out var parentMenuDto))
            {
                parentMenuDto.Children ??= [];

                parentMenuDto.Children.Add(menuDto);
            }
        }

        // 现在 menuDtos 包含了所有顶级菜单，每个顶级菜单都有其子菜单
        return menuDtos.Where(m => !m.SuperiorId.HasValue).ToList();
    }
}
