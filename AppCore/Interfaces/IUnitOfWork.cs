using AppCore.Models;

namespace AppCore.Interfaces;

public interface IUnitOfWork
{
    IGenericRepositoryAsync<Person> Persons { get; }
    IGenericRepositoryAsync<Company> Companies { get; }
}
