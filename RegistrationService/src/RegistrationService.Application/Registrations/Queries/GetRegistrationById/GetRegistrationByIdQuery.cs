using MediatR;

namespace RegistrationService.Application.Registrations.Queries.GetRegistrationById;

public sealed record GetRegistrationByIdQuery(Guid RegistrationId) : IRequest<RegistrationDto?>;

public sealed record RegistrationDto(
    Guid RegistrationId,
    Guid PatientId,
    string RegistrationNumber,
    DateTime RegisteredAtUtc,
    string Notes);
