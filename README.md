# NutriGuide - API Gateway

Single entry point for the NutriGuide application. The frontend calls this gateway instead of calling each backend service directly.

## What this project does

This service receives HTTP requests from the client and forwards them to the correct backend API (authentication, food catalog, meal plans, and others) using **Ocelot**.

It helps keep the frontend simple: one base URL for all API calls.

## Main features

- Route requests to User, Food, and Plan services
- Forward JWT tokens to protected endpoints
- Central `ocelot.json` configuration for all routes

## Technologies

- .NET 8
- ASP.NET Core
- Ocelot (API Gateway)

## Where this fits in NutriGuide

```
Frontend (nutri-guide-web)
        |
        v
API Gateway  <-- you are here
        |
   +----+----+----+
   |    |    |    |
 User Food Plan  (other services)
```

| Service | Repository | Role |
|---------|------------|------|
| Frontend | [nutri-guide-web](https://github.com/mwilaalexis/nutri-guide-web) | React UI |
| User / Auth | [nutri-guide-user-service](https://github.com/mwilaalexis/nutri-guide-user-service) | Login, profiles |
| Food | [nutri-guide-food-service](https://github.com/mwilaalexis/nutri-guide-food-service) | Foods and ingredients |
| Plans | [nutri-guide-plan-service](https://github.com/mwilaalexis/nutri-guide-plan-service) | Meal plans |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Backend services running locally (see their README files)
- (Optional) [nutri-guide-web](https://github.com/mwilaalexis/nutri-guide-web) for full-stack testing

## Run locally

1. Clone the repository:
   ```bash
   git clone https://github.com/mwilaalexis/nutri-guide-gateway.git
   cd nutri-guide-gateway
   ```
2. Start the other NutriGuide APIs first (user, food, plan services).
3. Open the gateway project (for example `test/test.Server`) in Visual Studio or VS Code.
4. Check ports in `ocelot.json` match your running services.
5. Run the project:
   ```bash
   dotnet run --project test/test.Server
   ```
6. Gateway base URL (default in config): `https://localhost:7059`
7. Test with Swagger or the frontend pointed at this URL.

## API overview (routed paths)

The gateway exposes grouped routes. Examples:

| Upstream (via gateway) | Sent to |
|------------------------|---------|
| `/api/auth/*` | User service |
| `/api/profile/*` | User service |
| `/api/foods/*`, `/api/ingredients/*` | Food service |
| `/api/plans/*` | Plan service |

Full routing rules are in `ocelot.json`.

## Suggested folder structure

```
nutri-guide-gateway/
├── test/
│   └── test.Server/     # ASP.NET Core host + Ocelot
│       ├── ocelot.json  # Route configuration
│       └── Program.cs
└── README.md
```

## Skills demonstrated

- REST APIs and HTTP routing
- API Gateway pattern (beginner level)
- Configuration-driven architecture
- Working in a multi-service student project

## Ideas to improve for recruiters

- [ ] Add a short `docs/` diagram of request flow
- [ ] Document exact local ports in a `SETUP.md` checklist
- [ ] Add a simple health endpoint on the gateway
- [ ] Pin a "How to run the full NutriGuide stack" note in the README

## Author

**Alex Mwila** - Junior .NET backend developer  
GitHub: [@mwilaalexis](https://github.com/mwilaalexis)
