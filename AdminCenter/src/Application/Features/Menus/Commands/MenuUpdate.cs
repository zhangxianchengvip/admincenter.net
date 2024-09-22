using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Application.Features.Organizations.Dto;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.DomainServices;
using AdminCenter.Domain.Enums;

namespace AdminCenter.Application.Features.Menus.Commands;

/// <summary>
/// 菜单修改
/// </summary>
public record MenuUpdateCommand(
        Guid Id,
        MenuTypeEnum MenuType,
        string Name,
        string? Route,
        bool IsLink,
        Guid? SuperiorId
    ) : IRequest<MenuDto>;

public class MenuUpdateHandler(IApplicationDbContext context, MenuManager manager) : IRequestHandler<MenuUpdateCommand, MenuDto>
{
    public async Task<MenuDto> Handle(MenuUpdateCommand request, CancellationToken cancellationToken)
    {
        var menu = await context.Menus.FindAsync(request.Id, cancellationToken);

        if (menu != null)
        {
            menu = await manager.UpdateAsync
            (
                menu: menu,
                name: request.Name,
                menuType: request.MenuType,
                route: request.Route,
                superiorId: request.SuperiorId,
                isLink: request.IsLink

            );

            return menu.Adapt<MenuDto>();
        }

        throw new BusinessException(ExceptionMessage.MenuNotExist);
    }
}
