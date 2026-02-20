namespace RegistrationService.Domain.Events;

public sealed record RegistrationCreatedEvent(
    Guid RegistrationId,
    Guid PatientId,
    string RegistrationNumber,
    DateTime RegisteredAtUtc,
    string Notes);
