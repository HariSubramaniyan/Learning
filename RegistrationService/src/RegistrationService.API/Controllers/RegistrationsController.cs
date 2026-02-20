using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Application.Registrations.Commands.CreateRegistration;
using RegistrationService.Application.Registrations.Queries.GetRegistrationById;

namespace RegistrationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class RegistrationsController : ControllerBase
{
    private readonly ISender _sender;

    public RegistrationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRegistration([FromBody] CreateRegistrationCommand command, CancellationToken cancellationToken)
    {
        var registrationId = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRegistrationById), new { id = registrationId }, registrationId);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RegistrationDto>> GetRegistrationById(Guid id, CancellationToken cancellationToken)
    {
        var registration = await _sender.Send(new GetRegistrationByIdQuery(id), cancellationToken);
        return registration is null ? NotFound() : Ok(registration);
    }
}
