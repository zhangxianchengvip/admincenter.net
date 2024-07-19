using AdminCenter.Application.Features.Users.Dto;

namespace AdminCenter.Application.Features.Users.Commands;

/// <summary>
/// 用户修改
/// </summary>
public record UserUpdateCommand(
    Guid Id,
    string LoginName,
    string RealName,
    string Password,
    string? NickName,
    string? Email,
    string? PhoneNumber,
    List<Guid> RoleIds,
    List<(Guid SuperiorOrganizationId, bool isSubsidiary)> SuperiorOrganizationIds) : IRequest<UserDto>;

public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
{
    public UserUpdateCommandValidator()
    {
        RuleFor(v => v.Id).NotNull();
        RuleFor(v => v.LoginName).NotNull();
        RuleFor(v => v.RealName).NotNull();
        RuleFor(v => v.Password).NotNull();
        RuleFor(v => v.RoleIds).Must(s => s.Any());
        RuleFor(v => v.SuperiorOrganizationIds).Must(s => s.Any());
    }
}

public class UserUpdateHandler(IApplicationDbContext context) : IRequestHandler<UserUpdateCommand, UserDto>
{
    public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id, cancellationToken);

        if (user != null)
        {
            user.Email = request.Email;
            user.NickName = request.NickName;
            user.PhoneNumber = request.PhoneNumber;

            user.UpdatePassword(request.Password);
            user.UpdateRealName(request.RealName);
            user.UpdateRoleRange(request.RoleIds);

            return user.Adapt<UserDto>();
        }

        throw new BusinessException(ExceptionMessage.UserNotExist);
    }
}

