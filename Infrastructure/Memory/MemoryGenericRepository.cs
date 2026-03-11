using AppCore.Interfaces;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T>: IGenericRepositoryAsync<T> 
    where T: class 
{
    protected Dictionary<Guid, T> _data = new();
    
    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<List<T>> FindAllAsync()
    {
        var result = _data.Values.ToList();
        return Task.FromResult(result);
    }

    public Task AddAsync(Guid id, T entity)
    {
        if (!_data.ContainsKey(id))
        {
            _data.Add(id, entity);
        }
        return Task.CompletedTask;
    }

    public Task RemoveByIdAsync(Guid id)
    {
        if (!_data.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Nie znaleziono encji o id: {id}");
        }
        _data.Remove(id);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, T entity)
    {
        if (!_data.ContainsKey(id))
        {
            throw new KeyNotFoundException($"Nie można zaktualizować. Brak encji o id: {id}");
        }
        _data[id] = entity;
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_data.ContainsKey(id));
    }
}