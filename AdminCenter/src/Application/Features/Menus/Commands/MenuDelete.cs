﻿
namespace AdminCenter.Application.Features.Menus.Commands;

/// <summary>
/// 菜单删除
/// </summary>
public record MenuDeleteCommand(Guid Id) : IRequest<bool>;

public class MenuDeleteHandler(IApplicationDbContext context) : IRequestHandler<MenuDeleteCommand, bool>
{
    public async Task<bool> Handle(MenuDeleteCommand request, CancellationToken cancellationToken)
    {
        var menu = await context.Menus
            .AsNoTracking()
            .Include(s => s.RoleMenu.Take(1))
            .FirstOrDefaultAsync(o => o.Id.Equals(request.Id));

        if (menu != null && !menu.RoleMenu.Any()) context.Menus.Remove(menu);

        if (menu == null) return true;

        throw new BusinessException(ExceptionMessage.MenuOccupy);
    }
}