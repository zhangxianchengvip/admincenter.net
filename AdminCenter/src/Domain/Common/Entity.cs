using AdminCenter.Domain.Constants;
using Ardalis.GuardClauses;

namespace AdminCenter.Domain.Common;

/// <summary>
/// 实体
/// </summary>
public abstract class Entity : IEntity
{

}

/// <summary>
/// 实体
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; init; } = default!;

    public Entity() { }

    public Entity(TKey id)
    {
        if (id == null)
        {
            throw new BusinessException("唯一标识不能为空");
        }

        Id = id;
    }
}
