namespace AdminCenter.Domain.Common;

/// <summary>
/// 值对象
/// </summary>
public interface IValueObject
{
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    bool EqualOperator(ValueObject left, ValueObject right);

    /// <summary>
    /// 不等与操作
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    bool NotEqualOperator(ValueObject left, ValueObject right);

    IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// 相等
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool Equals(object? obj);

    /// <summary>
    /// 获取Hash
    /// </summary>
    /// <returns></returns>
    int GetHashCode();
}
