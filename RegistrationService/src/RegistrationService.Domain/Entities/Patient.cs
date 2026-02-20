using RegistrationService.Domain.Common;

namespace RegistrationService.Domain.Entities;

public sealed class Patient : BaseEntity
{
    private readonly List<Registration> _registrations = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string Email { get; private set; }

    public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();

    private Patient() // EF Core
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public Patient(string firstName, string lastName, DateOnly dateOfBirth, string email)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        DateOfBirth = dateOfBirth;
        Email = email.Trim().ToLowerInvariant();
    }
}
