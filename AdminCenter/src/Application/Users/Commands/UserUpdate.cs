using AdminCenter.Application.Users.Dto;

namespace AdminCenter.Application;

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

        throw new AdminBusinessException(ExceptionMessage.UserNotExist);
    }
}

