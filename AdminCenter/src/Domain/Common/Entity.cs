using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCenter.Domain.Common;
public class Entity : IEntity
{

}

public class Entity<TKey> : Entity, IEntity<TKey>
{
    public TKey Id { get; init; }

    public Entity(TKey id)
    {
        Id = id;
    }
}
