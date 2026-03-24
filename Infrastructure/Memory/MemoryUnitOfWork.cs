using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.Memory;

namespace Infrastructure.Memory;

public class MemoryUnitOfWork : IUnitOfWork
{
    public IGenericRepositoryAsync<Person> Persons { get; } = new MemoryGenericRepository<Person>();
    public IGenericRepositoryAsync<Company> Companies { get; } = new MemoryGenericRepository<Company>();
}
