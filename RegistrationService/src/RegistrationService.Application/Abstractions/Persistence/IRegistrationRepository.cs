using RegistrationService.Domain.Entities;

namespace RegistrationService.Application.Abstractions.Persistence;

public interface IRegistrationRepository : IRepository<Registration>
{
    Task<bool> ExistsAsync(string registrationNumber, CancellationToken cancellationToken = default);
}
