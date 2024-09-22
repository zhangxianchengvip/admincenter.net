using AdminCenter.Application.Features.Roles.Dto;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.DomainServices;

namespace AdminCenter.Application.Features.Roles.Commands;

/// <summary>
/// 角色创建
/// </summary>
public record RoleCreateCommand(string Name, int Order, string? Description) : IRequest<RoleDto>;

public class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
{
    public RoleCreateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
    }
}

public class RoleCreateHandler(IApplicationDbContext context, RoleManager manager) : IRequestHandler<RoleCreateCommand, RoleDto>
{
    public async Task<RoleDto> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
    {
        var role = await manager.CreateAsync
        (
            request.Name,
            request.Order,
            request.Description
        );

        await context.Roles.AddAsync(role, cancellationToken);

        return role.Adapt<RoleDto>();
    }
}
