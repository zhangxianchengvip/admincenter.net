using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminCenter.Application.Common.Models;
using AdminCenter.Application.Features.Positions.Dtos;
using AdminCenter.Application.Features.Roles.Dto;
using AdminCenter.Domain.Common.Repository;
using CleanArchitecture.Application.Common.Mappings;

namespace AdminCenter.Application.Features.Positions.Queries;

/// <summary>
/// 职位列表
/// </summary>
public record PositionListQuery(int PageNumber, int PageSize) : IRequest<PaginatedList<PositionDto>>;

public class RoleListQueryValidator : AbstractValidator<PositionListQuery>
{
    public RoleListQueryValidator()
    {
        RuleFor(v => v.PageNumber).GreaterThan(0);
        RuleFor(v => v.PageSize).GreaterThan(5).LessThan(50);
    }
}

public class RoleListQueryHandler(IApplicationDbContext context) : IRequestHandler<PositionListQuery, PaginatedList<PositionDto>>
{
    public async Task<PaginatedList<PositionDto>> Handle(PositionListQuery request, CancellationToken cancellationToken)
    {
        return await context.Positions
        .ProjectToType<PositionDto>()
        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
