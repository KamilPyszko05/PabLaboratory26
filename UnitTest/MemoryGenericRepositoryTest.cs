using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;
using Infrastructure.Memory;

namespace UnitTest;

public class MemoryGenericRepositoryTest
{
    private readonly IGenericRepositoryAsync<Person> _repo = new MemoryGenericRepository<Person>();

    private Person CreateTestPerson(string firstName = "Adam", string lastName = "Nowak")
    {
        return new Person
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = "Jan",
            BirthDate = new DateTime(1990, 1, 1),
            Gender = Gender.Male,
            Position = "Developer",
            Organization = null,
            Employer = null
        };
    }

    [Fact]
    public async Task AddPersonTestAsync()
    {

        var expected = CreateTestPerson();
        

        await _repo.AddAsync(expected);
        

        var actual = await _repo.FindByIdAsync(expected.Id);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Id, actual?.Id);
    }

    [Fact]
    public async Task FindAllAsync_ShouldReturnAllPersons()
    {

        var person1 = CreateTestPerson("Adam", "Nowak");
        var person2 = CreateTestPerson("Ewa", "Kowalska");
        await _repo.AddAsync(person1);
        await _repo.AddAsync(person2);


        var result = await _repo.FindAllAsync();


        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdatePerson()
    {

        var person = CreateTestPerson();
        await _repo.AddAsync(person);
        person.FirstName = "John";


        await _repo.UpdateAsync(person);
        var updatedPerson = await _repo.FindByIdAsync(person.Id);


        Assert.Equal("John", updatedPerson?.FirstName);
    }

    [Fact]
    public async Task RemoveByIdAsync_ShouldRemovePerson()
    {

        var person = CreateTestPerson();
        await _repo.AddAsync(person);


        await _repo.RemoveByIdAsync(person.Id);
        var result = await _repo.FindByIdAsync(person.Id);


        Assert.Null(result);
    }

    [Fact]
    public async Task FindPagedAsync_ShouldReturnPagedResult()
    {

        for (int i = 0; i < 5; i++)
        {
            await _repo.AddAsync(CreateTestPerson($"Person{i}", $"Test{i}"));
        }

        int page = 2;
        int pageSize = 2;

        var result = await _repo.FindPagedAsync(page, pageSize);


        Assert.Equal(2, result.Items.Count);
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);
        Assert.True(result.HasNext);
        Assert.True(result.HasPrevious);
    }
}