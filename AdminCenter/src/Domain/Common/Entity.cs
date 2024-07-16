namespace AdminCenter.Domain.Common;

/// <summary>
/// 实体
/// </summary>
public class Entity : IEntity
{

}

/// <summary>
/// 实体
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; init; }

    public Entity(TKey id)
    {
        Id = id;
    }
}
