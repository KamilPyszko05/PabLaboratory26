using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T>: IGenericRepositoryAsync<T> 
    where T: EntityBase
{
    protected Dictionary<Guid, T> _data = new();
    
    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        return Task.FromResult(_data.Values.AsEnumerable());
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        var totalCount = _data.Count;
        var items = _data.Values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PagedResult<T>(items, totalCount, page, pageSize);
        return Task.FromResult(result);
    }

    public Task<T> AddAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        if (_data.ContainsKey(entity.Id))
        {
            throw new ArgumentException($"Encja o id {entity.Id} już istnieje w repozytorium.");
        }

        _data.Add(entity.Id, entity);
        return Task.FromResult(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (!_data.ContainsKey(entity.Id))
        {
            throw new KeyNotFoundException($"Nie można zaktualizować. Brak encji o id: {entity.Id}");
        }

        _data[entity.Id] = entity;
        return Task.FromResult(entity);
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