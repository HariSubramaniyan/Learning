namespace RegistrationService.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateServiceToken(string subject, IEnumerable<string> roles);
}
