using AppCore.Interfaces;
using AppCore.Mappings;
using AppCore.Services;
using AppCore.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Memory;

namespace WebApi.Modules;

public static class ContactsModule
{
    public static IServiceCollection AddContactsModule(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, MemoryUnitOfWork>();

        services.AddScoped<IPersonService, PersonService>();

        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(PersonProfile).Assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();

        return services;
    }
}
