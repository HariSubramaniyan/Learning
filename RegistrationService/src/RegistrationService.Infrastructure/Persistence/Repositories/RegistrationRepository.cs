using Microsoft.EntityFrameworkCore;
using RegistrationService.Application.Abstractions.Persistence;
using RegistrationService.Domain.Entities;

namespace RegistrationService.Infrastructure.Persistence.Repositories;

public sealed class RegistrationRepository : Repository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(RegistrationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsAsync(string registrationNumber, CancellationToken cancellationToken = default)
        => await DbContext.Registrations.AnyAsync(
            x => x.RegistrationNumber == registrationNumber.ToUpper(),
            cancellationToken);
}
