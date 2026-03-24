using AppCore.Dto;

namespace AppCore.Interfaces;

public interface IPersonService
{
    Task<PersonDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<PersonDto>> GetAllAsync();
    Task<PersonDto> AddPersonAsync(CreatePersonDto dto);
    Task<PersonDto?> UpdatePersonAsync(Guid id, UpdatePersonDto dto);
}
