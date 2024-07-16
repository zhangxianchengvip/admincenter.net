namespace AdminCenter.Domain.Common;

public interface IAuditableEntity : IEntity
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}

public interface IAuditableEntity<TKey> : IAuditableEntity, IEntity<TKey>
{

}
