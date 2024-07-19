using AdminCenter.Application.Roles.Dto;
using AdminCenter.Domain.DomainServices;

namespace AdminCenter.Application.Roles.Commands;
public record RoleCreateCommand(string Name, string? Description) : IRequest<RoleDto>;

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
            request.Description
        );

        await context.Roles.AddAsync(role, cancellationToken);

        return role.Adapt<RoleDto>();
    }
}
