using System.Diagnostics.CodeAnalysis;

namespace AdminCenter.Domain.Common;


public interface IEntity
{

}

public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; init; }
}
