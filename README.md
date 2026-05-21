# NutriGuide Gateway

API gateway for the [NutriGuide](https://github.com/mwilaalexis/nutri-guide-web) platform. Routes client requests to backend microservices using Ocelot.

## Role in the system

| Service | Repository |
|---------|------------|
| Frontend | [nutri-guide-web](https://github.com/mwilaalexis/nutri-guide-web) |
| User profile and auth | [nutri-guide-user-service](https://github.com/mwilaalexis/nutri-guide-user-service) |
| Food catalog | [nutri-guide-food-service](https://github.com/mwilaalexis/nutri-guide-food-service) |
| Meal plans | [nutri-guide-plan-service](https://github.com/mwilaalexis/nutri-guide-plan-service) |

## Tech stack

- .NET 8
- ASP.NET Core
- Ocelot

## Configuration

Routes are defined in `ocelot.json`. Upstream paths are mapped to downstream microservice hosts and ports. JWT-protected routes forward the Authorization header to backend services.

## Run locally

```bash
dotnet run
```

Ensure dependent services are running on the ports referenced in `ocelot.json`.

## Related repositories

Part of the NutriGuide microservices suite by [mwilaalexis](https://github.com/mwilaalexis).
