using RegistrationService.Domain.Entities;

namespace RegistrationService.Application.Abstractions.Persistence;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
