using MediatR;
using RegistrationService.Application.Abstractions.Persistence;
using RegistrationService.Domain.Entities;

namespace RegistrationService.Application.Patients.Commands.CreatePatient;

public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await _patientRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingPatient is not null)
        {
            return existingPatient.Id;
        }

        var patient = new Patient(request.FirstName, request.LastName, request.DateOfBirth, request.Email);
        await _patientRepository.AddAsync(patient, cancellationToken);
        await _patientRepository.SaveChangesAsync(cancellationToken);

        return patient.Id;
    }
}
