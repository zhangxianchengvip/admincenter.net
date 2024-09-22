using AdminCenter.Application.Features.Positions.Dtos;
using AdminCenter.Domain.Common.Repository;
using AdminCenter.Domain.DomainServices;

namespace AdminCenter.Application.Features.Positions.Commands;

/// <summary>
/// 创建职位
/// </summary>
public record PositionCreateCommand(
    string Name,
    string Code,
    string? Description) : IRequest<PositionDto>;


public class PositionCreateCommandValidator : AbstractValidator<PositionCreateCommand>
{
    public PositionCreateCommandValidator()
    {
        RuleFor(v => v.Name).NotNull();
        RuleFor(v => v.Code).NotNull();
    }
}

public class PositionCreateHandler(IApplicationDbContext context, PositionManager manager) : IRequestHandler<PositionCreateCommand, PositionDto>
{
    public async Task<PositionDto> Handle(PositionCreateCommand request, CancellationToken cancellationToken)
    {
        var position = await manager.CreateAsync
        (
            request.Name,
            request.Code,
            request.Description
        );

        await context.Positions.AddAsync(position, cancellationToken);

        return position.Adapt<PositionDto>();
    }
}

