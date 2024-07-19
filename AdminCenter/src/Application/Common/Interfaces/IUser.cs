namespace AdminCenter.Application.Common.Interfaces;

public interface IUser<TKey>
{
    TKey? Id { get; }
}
