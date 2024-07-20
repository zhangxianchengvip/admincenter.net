using AdminCenter.Application.Features.Positions.Dtos;
using AdminCenter.Application.Features.Roles.Commands;
using AdminCenter.Domain.DomainServices;

namespace AdminCenter.Application.Features.Positions.Commands;
public record PositionUpdateCommand(
    Guid Id,
    string Name,
    string Code,
    string? Description) : IRequest<PositionDto>;


public class PositionUpdateCommandValidator : AbstractValidator<PositionUpdateCommand>
{
    public PositionUpdateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
    }
}

public class PositionUpdateHandler(IApplicationDbContext context, PositionManager manager) : IRequestHandler<PositionUpdateCommand, PositionDto>
{
    public async Task<PositionDto> Handle(PositionUpdateCommand request, CancellationToken cancellationToken)
    {
        var position = await context.Positions.FindAsync(request.Id);

        if (position != null)
        {
            position = await manager.UpdateAsync
            (
                position: position,
                name: request.Name,
                code: request.Code,
                description: request.Description
            );

            return position.Adapt<PositionDto>();
        }

        throw new BusinessException(ExceptionMessage.PositionNotExist);
    }
}


