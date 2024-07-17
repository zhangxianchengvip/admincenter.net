using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Application.Users.Dto;
using AdminCenter.Domain;
using AdminCenter.Domain.Constants;
using AdminCenter.Domain.Exceptions;
using Mapster;

namespace AdminCenter.Application;

public record UpdateUserCommand(
Guid Id,
string LoginName,
string RealName,
string Password,
string? NickName,
string? Email,
string? PhoneNumber,
List<Guid> RoleIds,
List<(Guid SuperiorOrganizationId, bool isSubsidiary)> SuperiorOrganizationIds
) : IRequest<UserDto>;



public class UpdateUserHandler(IApplicationDbContext context) : IRequestHandler<UpdateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id,cancellationToken);

        if (user == null)
        {
            throw new AdminBusinessException(ExctptionMessage.UserNotExist);
        }

        user.Email = request.Email;
        user.NickName = request.NickName;
        user.PhoneNumber = request.PhoneNumber;

        user.UpdatePassword(request.Password);
        user.UpdateRealName(request.RealName);
        user.UpdateRoleRange(request.RoleIds);

        return user.Adapt<UserDto>();
    }
}

