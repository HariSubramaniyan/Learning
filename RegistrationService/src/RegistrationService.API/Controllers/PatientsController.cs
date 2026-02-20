using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Application.Patients.Commands.CreatePatient;

namespace RegistrationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class PatientsController : ControllerBase
{
    private readonly ISender _sender;

    public PatientsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePatient([FromBody] CreatePatientCommand command, CancellationToken cancellationToken)
    {
        var patientId = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(CreatePatient), new { id = patientId }, patientId);
    }
}
