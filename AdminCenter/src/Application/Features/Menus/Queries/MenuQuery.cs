using AdminCenter.Application.Features.Menus.Dtos;
using AdminCenter.Application.Features.Organizations.Dto;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Menus.Queries;

/// <summary>
/// 组织查询
/// </summary>
public record MenuQuery(Guid Id) : IRequest<MenuDto>;

public class MenuQueryHandler(IApplicationDbContext context) : IRequestHandler<MenuQuery, MenuDto>
{
    public async Task<MenuDto> Handle(MenuQuery request, CancellationToken cancellationToken)
    {
        var menu = await context.Menus.FindAsync(request.Id);
        return menu != null ? menu.Adapt<MenuDto>() : throw new BusinessException(ExceptionMessage.MenuNotExist);
    }
}
