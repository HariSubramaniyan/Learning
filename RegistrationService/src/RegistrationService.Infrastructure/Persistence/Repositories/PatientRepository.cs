using Microsoft.EntityFrameworkCore;
using RegistrationService.Application.Abstractions.Persistence;
using RegistrationService.Domain.Entities;

namespace RegistrationService.Infrastructure.Persistence.Repositories;

public sealed class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(RegistrationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await DbContext.Patients.FirstOrDefaultAsync(x => x.Email == email.ToLower(), cancellationToken);
}
