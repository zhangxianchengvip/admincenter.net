using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCenter.Domain.Common;
public class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}


public class AuditableEntity<TKey> : AuditableEntity, IAuditableEntity<TKey>
{
    public TKey Id { get; init; }
    public AuditableEntity(TKey id)
    {
        Id = id;
    }
}
