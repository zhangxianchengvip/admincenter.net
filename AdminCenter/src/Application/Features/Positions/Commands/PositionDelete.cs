namespace AdminCenter.Application.Features.Positions.Commands;
public record PositionDeleteCommand(Guid Id) : IRequest<bool>;


public class PositionDeleteHandler(IApplicationDbContext context) : IRequestHandler<PositionDeleteCommand, bool>
{
    public async Task<bool> Handle(PositionDeleteCommand request, CancellationToken cancellationToken)
    {
        var position = await context.Positions
            .Include(s => s.UserPositions.Take(1))
            .FirstOrDefaultAsync(s => s.Id.Equals(request.Id));

        if (position != null && position.UserPositions.Count == 0) context.Positions.Remove(position);

        return position == null ? true : throw new BusinessException(ExceptionMessage.RoleOccupy);
    }
}



