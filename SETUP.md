# Getting Started

Quick setup guide to get this project running locally.

---

## Prerequisites

You need:
- **.NET 9.0 SDK** — [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Docker** (recommended) or PostgreSQL 16+ running locally
- **EF Core CLI tools** (for migrations)

Install EF Core CLI if you don't have it:
```bash
dotnet tool install --global dotnet-ef
```

---

## 1. Set Up the Database

We're using PostgreSQL. The easiest path is Docker Compose (already configured).

### Option A: Docker Compose (Recommended)

```bash
# From project root
docker-compose up -d
```

This spins up a Postgres 16 container with the credentials from your `.env` file (see next section).

Check it's healthy:
```bash
docker ps
# Look for the `natura_technical_test` container; status should be "healthy"
```

---

## 2. Configure Environment Variables

Copy the example env file:

```bash
cp .env.example .env
```

Edit `.env` if needed. Defaults work for Docker Compose:

```bash
POSTGRES_HOST=localhost
POSTGRES_PORT=5432
POSTGRES_DB=natura_test_dev
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
```

---

## 3. Run Migrations

Apply the schema to your database:

```bash
dotnet ef database update
```

If you need to create new migrations later:
```bash
dotnet ef migrations add YourMigrationName
```

---

## 4. Run the App

From the project root:

```bash
dotnet run
```

The app should start and listen on `http://localhost:5000`. You'll see output like:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

Open your browser and hit the API. Navigate to `/scalar/v1` to explore the endpoints.

---
