using RegistrationService.Domain.Common;

namespace RegistrationService.Domain.Entities;

public sealed class Registration : BaseEntity
{
    public Guid PatientId { get; private set; }
    public string RegistrationNumber { get; private set; }
    public DateTime RegisteredAtUtc { get; private set; }
    public string Notes { get; private set; }

    public Patient? Patient { get; private set; }

    private Registration() // EF Core
    {
        RegistrationNumber = string.Empty;
        Notes = string.Empty;
    }

    public Registration(Guid patientId, string registrationNumber, string notes)
    {
        PatientId = patientId;
        RegistrationNumber = registrationNumber.Trim().ToUpperInvariant();
        Notes = notes.Trim();
        RegisteredAtUtc = DateTime.UtcNow;
    }
}
