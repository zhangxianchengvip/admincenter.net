namespace AdminCenter.Application;

public class OrganizationDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// 上级组织
    /// </summary>
    public Guid? SuperiorOrganizationId { get; set; }
}
