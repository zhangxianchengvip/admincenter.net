using AdminCenter.Application.Common.Interfaces;
using AdminCenter.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain.DomainServices;
public class PositionManager(IApplicationDbContext context) : DomainService
{
    /// <summary>
    /// 创建角色
    /// </summary>
    public async Task<Position> CreateAsync(
        [NotNull] string name,
        [NotNull] string code,
        string? description = null)
    {
        var position = new Position
        (
            id: Guid.NewGuid(),
            name: name,
            code: code,
            description: description
        );

        var exist = await context.Roles.AnyAsync(s => s.Name.Equals(name));

        return !exist ? position : throw new BusinessException(ExceptionMessage.PositionCodeExist);
    }

    /// <summary>
    /// 角色修改
    /// </summary>
    public async Task<Position> UpdateAsync(
        [NotNull] Position position,
        [NotNull] string name,
        [NotNull] string code,
        string? description = null)
    {
        position.UpdatePositionName(name);
        position.UpdatePositionCode(code);
        position.Description = description;

        var exist = await context.Positions.AnyAsync(s => s.Code.Equals(code));

        return !exist ? position : throw new BusinessException(ExceptionMessage.PositionCodeExist);
    }
}
