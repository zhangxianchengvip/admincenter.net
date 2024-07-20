using AdminCenter.Application.Features.Positions.Dtos;

namespace AdminCenter.Application.Features.Positions.Queries;

/// <summary>
/// 职位查询
/// </summary>
public record PositionQuery(Guid Id) : IRequest<PositionDto>;


public class PositionQueryHandler(IApplicationDbContext context) : IRequestHandler<PositionQuery, PositionDto>
{
    public async Task<PositionDto> Handle(PositionQuery request, CancellationToken cancellationToken)
    {
        var role = await context.Positions.FindAsync(request.Id);

        return role != null ? role.Adapt<PositionDto>() : throw new BusinessException(ExceptionMessage.PositionNotExist);
    }
}

