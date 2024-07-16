using System.Diagnostics.CodeAnalysis;
using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain;

/// <summary>
/// 用户角色
/// </summary>
public class UserRole : AuditableEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; private set; }

    public UserRole([NotNull] Guid userId, [NotNull] Guid roleId)
    {
        UserId = Guard.Against.NullOrEmpty
        (
            input: userId,
            parameterName: nameof(userId),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.UserIdNull)
        );

        RoleId = Guard.Against.NullOrEmpty
        (
            input: roleId,
            parameterName: nameof(roleId),
            exceptionCreator: () => new AdminBusinessException(ExctptionMessage.RoleIdNull)
        );
    }
}
