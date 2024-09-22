namespace AdminCenter.Domain.Common.Entities;

/// <summary>
/// 审计实体
/// </summary>
public class AuditableEntity : IAuditableEntity
{

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTimeOffset LastModified { get; set; }

    /// <summary>
    /// 最后修改人
    /// </summary>
    public string? LastModifiedBy { get; set; }
}


/// <summary>
/// 审计实体
/// </summary>
public class AuditableEntity<TKey> : AuditableEntity, IAuditableEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; init; } = default!;

    public AuditableEntity() { }

    public AuditableEntity(TKey id)
    {
        if (id == null)
        {
            throw new BusinessException("唯一标识不能为空");
        }

        Id = id;
    }
}
