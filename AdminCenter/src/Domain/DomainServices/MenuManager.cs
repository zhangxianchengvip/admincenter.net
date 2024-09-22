
using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Common.Domain;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Entities;
using AdminCenter.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Domain.DomainServices;
public class MenuManager(IApplicationDbContext context) : DomainService
{
    public async Task<Menu> CreateAsync(
        [NotNull] MenuTypeEnum menuType,
        [NotNull] string name,
        string? route,
        bool isLink,
        Guid? superiorId)
    {
        var menu = new Menu
        (
            id: Guid.NewGuid(),
            menuType: menuType,
            name: name,
            route: route,
            isLink: isLink,
            superiorId: superiorId
        );

        var exist = await context.Menus.AnyAsync(s => s.Name.Equals(name));

        return !exist ? menu : throw new BusinessException(ExceptionMessage.MenuExist);
    }

    public async Task<Menu> UpdateAsync(
        [NotNull] Menu menu,
        [NotNull] MenuTypeEnum menuType,
        [NotNull] string name,
        string? route,
        bool isLink,
        Guid? superiorId)
    {
        menu.Route = route;
        menu.IsLink = isLink;
        menu.UpdateName(name);
        menu.MenuType = menuType;
        menu.SuperiorId = superiorId;

        var exist = await context.Menus.AnyAsync(s => s.Name.Equals(name));

        return !exist ? menu : throw new BusinessException(ExceptionMessage.MenuExist);
    }
}
