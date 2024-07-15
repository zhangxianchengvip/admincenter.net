using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain.Common;


public abstract class Entity
{

}

public abstract class Entity<T> : Entity
{
    public T Id { get; init; }

    protected Entity([NotNull] T id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        Id = id;
    }
}
