using Core.Entities;
using Entity;

namespace Service;

public interface IDbOperationEvent<T, TId> where T : IEntity<TId>, new()
{
    ITBaseService<T,TId> Current { get; }
}