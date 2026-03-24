using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.ValueObjects;
using AutoMapper;

namespace AppCore.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PersonDto?> GetByIdAsync(Guid id)
    {
        var person = await _unitOfWork.Persons.FindByIdAsync(id);
        return person is null ? null : _mapper.Map<PersonDto>(person);
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        var persons = await _unitOfWork.Persons.FindAllAsync();
        return _mapper.Map<IEnumerable<PersonDto>>(persons);
    }

    public async Task<PersonDto> AddPersonAsync(CreatePersonDto dto)
    {
        var person = new Person
        {
            FirstName    = dto.FirstName,
            LastName     = dto.LastName,
            MiddleName   = string.Empty,
            BirthDate    = dto.BirthDate,
            Gender       = dto.Gender,
            Position     = dto.Position,
            Organization = null,
            Employer     = dto.EmployerId.HasValue
                ? await _unitOfWork.Companies.FindByIdAsync(dto.EmployerId.Value)
                : null
        };

        var added = await _unitOfWork.Persons.AddAsync(person);
        return _mapper.Map<PersonDto>(added);
    }

    public async Task<PersonDto?> UpdatePersonAsync(Guid id, UpdatePersonDto dto)
    {
        var person = await _unitOfWork.Persons.FindByIdAsync(id);
        if (person is null) return null;

        if (dto.FirstName is not null) person.FirstName = dto.FirstName;
        if (dto.LastName  is not null) person.LastName  = dto.LastName;
        if (dto.BirthDate.HasValue)    person.BirthDate = dto.BirthDate;
        if (dto.Gender.HasValue)       person.Gender    = dto.Gender.Value;
        if (dto.Position is not null)  person.Position  = dto.Position;

        if (dto.EmployerId.HasValue)
        {
            person.Employer = await _unitOfWork.Companies.FindByIdAsync(dto.EmployerId.Value);
        }

        var updated = await _unitOfWork.Persons.UpdateAsync(person);
        return _mapper.Map<PersonDto>(updated);
    }
}
