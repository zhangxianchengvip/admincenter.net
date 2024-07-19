using AdminCenter.Application.Users.Dto;
using AdminCenter.Domain;

namespace AdminCenter.Application;

/// <summary>
/// 创建用户
/// </summary>
public record UserCreateCommand(
    string LoginName,
    string RealName,
    string Password,
    string? NickName,
    string? Email,
    string? PhoneNumber,
    List<Guid> RoleIds,
    List<(Guid SuperiorOrganizationId, bool isSubsidiary)> SuperiorOrganizationIds) : IRequest<UserDto>;

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(v => v.LoginName).NotNull();
        RuleFor(v => v.RealName).NotNull();
        RuleFor(v => v.Password).NotNull();
        RuleFor(v => v.RoleIds).Must(s => s.Any());
        RuleFor(v => v.SuperiorOrganizationIds).Must(s => s.Any());
    }
}


public class CreateUserHandler(IApplicationDbContext context, UserManager manager) : IRequestHandler<UserCreateCommand, UserDto>
{
    public async Task<UserDto> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = await manager.CreateAsync
        (
            request.LoginName,
            request.RealName,
            request.Password,
            request.NickName,
            request.Email,
            request.PhoneNumber,
            request.RoleIds,
            request.SuperiorOrganizationIds
        );

        await context.Users.AddAsync(user, cancellationToken);

        return user.Adapt<UserDto>();
    }
}

