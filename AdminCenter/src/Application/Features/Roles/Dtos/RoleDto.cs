namespace AdminCenter.Application.Features.Roles.Dto;
public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
