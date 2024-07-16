
using AdminCenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminCenter.Application.Common.Interfaces;

public interface IApplicationDbContext<T, KeyType> where T : AggregateRoot<KeyType>
{
    DbSet<T> Data { get; }
}
