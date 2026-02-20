using MediatR;

namespace RegistrationService.Application.Patients.Commands.CreatePatient;

public sealed record CreatePatientCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Email) : IRequest<Guid>;
