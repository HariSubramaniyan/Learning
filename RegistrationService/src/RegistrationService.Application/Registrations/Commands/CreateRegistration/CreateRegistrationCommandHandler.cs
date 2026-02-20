using MediatR;
using RegistrationService.Application.Abstractions.Messaging;
using RegistrationService.Application.Abstractions.Persistence;
using RegistrationService.Domain.Entities;
using RegistrationService.Domain.Events;

namespace RegistrationService.Application.Registrations.Commands.CreateRegistration;

public sealed class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEventPublisher _eventPublisher;

    public CreateRegistrationCommandHandler(
        IPatientRepository patientRepository,
        IRegistrationRepository registrationRepository,
        IEventPublisher eventPublisher)
    {
        _patientRepository = patientRepository;
        _registrationRepository = registrationRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Guid> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken)
            ?? throw new InvalidOperationException("Patient does not exist.");

        var alreadyExists = await _registrationRepository.ExistsAsync(request.RegistrationNumber, cancellationToken);
        if (alreadyExists)
        {
            throw new InvalidOperationException("Registration number already exists.");
        }

        var registration = new Registration(patient.Id, request.RegistrationNumber, request.Notes);
        await _registrationRepository.AddAsync(registration, cancellationToken);
        await _registrationRepository.SaveChangesAsync(cancellationToken);

        var registrationCreatedEvent = new RegistrationCreatedEvent(
            registration.Id,
            registration.PatientId,
            registration.RegistrationNumber,
            registration.RegisteredAtUtc,
            registration.Notes);

        await _eventPublisher.PublishAsync(registrationCreatedEvent, cancellationToken);

        return registration.Id;
    }
}
