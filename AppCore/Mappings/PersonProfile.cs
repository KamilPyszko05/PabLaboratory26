using AppCore.Dto;
using AppCore.Models;
using AppCore.ValueObjects;
using AutoMapper;

namespace AppCore.Mappings;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Address, AddressDto>()
            .ConstructUsing(src => new AddressDto(
                src.Street,
                src.City,
                src.PostalCode,
                src.Country.ToString(),
                src.Type));

        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.FirstName,   opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,    opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDate,   opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Gender,      opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Position,    opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.EmployerId,  opt => opt.MapFrom(src =>
                src.Employer != null ? (Guid?)src.Id : null))
            .ForMember(dest => dest.Id,          opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email,       opt => opt.Ignore())
            .ForMember(dest => dest.Phone,       opt => opt.Ignore())
            .ForMember(dest => dest.Address,     opt => opt.Ignore())
            .ForMember(dest => dest.Status,      opt => opt.Ignore())
            .ForMember(dest => dest.Tags,        opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt,   opt => opt.Ignore());
    }
}
