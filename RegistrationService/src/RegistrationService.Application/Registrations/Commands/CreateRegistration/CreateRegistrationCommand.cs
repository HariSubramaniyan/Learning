using MediatR;

namespace RegistrationService.Application.Registrations.Commands.CreateRegistration;

public sealed record CreateRegistrationCommand(
    Guid PatientId,
    string RegistrationNumber,
    string Notes) : IRequest<Guid>;
