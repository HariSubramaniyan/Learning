using MediatR;
using RegistrationService.Application.Abstractions.Persistence;

namespace RegistrationService.Application.Registrations.Queries.GetRegistrationById;

public sealed class GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, RegistrationDto?>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationByIdQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<RegistrationDto?> Handle(GetRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var registration = await _registrationRepository.GetByIdAsync(request.RegistrationId, cancellationToken);

        return registration is null
            ? null
            : new RegistrationDto(
                registration.Id,
                registration.PatientId,
                registration.RegistrationNumber,
                registration.RegisteredAtUtc,
                registration.Notes);
    }
}
