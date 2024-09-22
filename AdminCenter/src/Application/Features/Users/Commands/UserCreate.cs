using AdminCenter.Application.Features.Users.Dto;
using AdminCenter.Domain;
using AdminCenter.Domain.Common.Repository;

namespace AdminCenter.Application.Features.Users.Commands;

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
    List<OrganizationInfo> OrganizationIds) : IRequest<UserDto>;

public record OrganizationInfo(Guid OrganizationId, bool IsSubsidiary);
public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(v => v.LoginName).NotNull();
        RuleFor(v => v.RealName).NotNull();
        RuleFor(v => v.Password).NotNull();
        RuleFor(v => v.RoleIds).Must(s => s.Count != 0);
        RuleFor(v => v.OrganizationIds).Must(s => s.Count != 0);
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
            request.OrganizationIds.Select(o => (o.OrganizationId, o.IsSubsidiary)).ToList()
        );

        await context.Users.AddAsync(user, cancellationToken);

        return user.Adapt<UserDto>();
    }
}

