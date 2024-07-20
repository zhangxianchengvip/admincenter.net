using System.Diagnostics.CodeAnalysis;
using AdminCenter.Application.Features.Organizations.Commands;
using AdminCenter.Domain;
using AdminCenter.Domain.DomainServices;
using AdminCenter.Domain.Enums;

namespace AdminCenter.Application.Features.Menus.Commands;

/// <summary>
/// 菜单创建
/// </summary>
public record MenuCreateCommand(
        MenuTypeEnum MenuType,
        string Name,
        string? Route,
        bool IsLink,
        Guid? SuperiorId
    ) : IRequest<bool>;


public class MenuCreateCommandValidator : AbstractValidator<MenuCreateCommand>
{
    public MenuCreateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
    }
}
public class OrganizationCreateHandler(IApplicationDbContext context, MenuManager manager) : IRequestHandler<MenuCreateCommand, bool>
{
    public async Task<bool> Handle(MenuCreateCommand request, CancellationToken cancellationToken)
    {
        var menu = await manager.CreateAsync
        (
            request.MenuType,
            request.Name,
            request.Route,
            request.IsLink,
            request.SuperiorId
        );

        await context.Menus.AddAsync(menu);

        return true;
    }
}
