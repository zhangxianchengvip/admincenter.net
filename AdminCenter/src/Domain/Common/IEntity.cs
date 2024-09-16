using System.ComponentModel.DataAnnotations;

namespace AdminCenter.Domain.Common;

//复合主键或无主键标识实体基类
public interface IEntity
{

}

//单一主键标识实体基类
public interface IEntity<TKey> : IEntity
{
    [Key]
    public TKey Id { get; init; }
}
