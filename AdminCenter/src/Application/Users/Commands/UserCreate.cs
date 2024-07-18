using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Domain;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application;

/// <summary>
/// 创建用户
/// </summary>
/// <param name="LoginName"></param>
/// <param name="RealName"></param>
/// <param name="Password"></param>
/// <param name="NickName"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
/// <param name="RoleIds"></param>
/// <param name="SuperiorOrganizationIds"></param>
public record UserCreateCommand(
    string LoginName,
    string RealName,
    string Password,
    string? NickName,
    string? Email,
    string? PhoneNumber,
    List<Guid> RoleIds,
    List<(Guid SuperiorOrganizationId, bool isSubsidiary)> SuperiorOrganizationIds) : IRequest<UserDto>;

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

