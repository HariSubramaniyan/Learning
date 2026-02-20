# RegistrationService (.NET 8 Web API)

RegistrationService is a microservice-style Web API built with Clean Architecture principles.

## Architecture

- **RegistrationService.API**: HTTP layer (controllers, auth pipeline, Swagger).
- **RegistrationService.Application**: CQRS handlers (MediatR), repository abstractions, and use cases.
- **RegistrationService.Domain**: Core entities (`Patient`, `Registration`) and domain event payload (`RegistrationCreatedEvent`).
- **RegistrationService.Infrastructure**: EF Core + SQL Server, RabbitMQ publisher, JWT token generation, repository implementations.

## Features

- Clean Architecture separation
- CQRS with MediatR
- EF Core with SQL Server provider
- Async repository pattern
- RabbitMQ integration to publish `RegistrationCreatedEvent` after registration creation
- JWT Authentication/Authorization
- Swagger/OpenAPI enabled in Development

## Running (local prerequisites)

1. Install .NET 8 SDK
2. Configure SQL Server and RabbitMQ
3. Update `src/RegistrationService.API/appsettings.json`
4. Run the API project:

```bash
dotnet run --project src/RegistrationService.API/RegistrationService.API.csproj
```

## API Endpoints

- `POST /api/auth/token` - generate JWT for testing
- `POST /api/patients` - create or get existing patient by email
- `POST /api/registrations` - create registration (publishes event to RabbitMQ)
- `GET /api/registrations/{id}` - get registration by id

> Except `/api/auth/token`, endpoints require `Authorization: Bearer <token>`.
