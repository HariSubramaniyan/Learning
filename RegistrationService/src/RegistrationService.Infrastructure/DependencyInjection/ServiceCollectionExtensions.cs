using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationService.Application.Abstractions.Authentication;
using RegistrationService.Application.Abstractions.Messaging;
using RegistrationService.Application.Abstractions.Persistence;
using RegistrationService.Infrastructure.Authentication;
using RegistrationService.Infrastructure.Messaging;
using RegistrationService.Infrastructure.Persistence;
using RegistrationService.Infrastructure.Persistence.Repositories;

namespace RegistrationService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RegistrationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RegistrationDatabase")));

        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
