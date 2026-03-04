using AppCore.Interfaces;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T>: IGenericRepositoryAsync<T> 
    where T: class 
{
    private Dictionary<Guid, T> _data = new();
    
    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<List<T>> FindAllAsync()
    {
        ...
    }

    public Task RemoveByIdAsync(Guid id)
    {
        ...
    }

    public Task UpdateAsync(Guid id, T o)
    {
        ...        
    }
    // pozostałe metody
}