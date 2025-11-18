# Project Scaffolding Plan

## 1. Restated Task

Scaffold a .NET 9 Web API project with:
- Clean, feature-based folder structure suitable for junior developers
- Dependency injection configured for EF Core with PostgreSQL
- Database infrastructure (entities, migrations, seeding) using a User table as the initial example
- Endpoint organization where each feature/endpoint lives in its own folder

---

## 2. Goal / Scope / Deliverable

**Goal**: Create a maintainable, scalable project template that demonstrates best practices for junior developers

**Scope**:
- ✅ Folder structure (feature-based vertical slices)
- ✅ DI container setup for EF Core + Postgres
- ✅ DbContext and connection string configuration
- ✅ User entity, migration, and seed data
- ✅ Example endpoint structure (folder per feature)
- ❌ Authentication/authorization (can add later)
- ❌ Advanced patterns (CQRS, MediatR) — keep it simple

**Deliverable**:
- Modified `Program.cs` with DI wiring
- New folder structure with organized directories
- EF Core entities, DbContext, migrations
- Configuration files (appsettings for connection strings)
- At least one example feature endpoint to demonstrate the pattern

---

## 3. CTO-Level Judgment

**Key Assumptions**:
- Using **vertical slice architecture** (feature folders) over traditional layered architecture — better for team autonomy and feature locality
- Starting simple with minimal abstractions — juniors can add repository pattern later if needed; direct DbContext usage is fine for learning

**Trade-offs**:
- **Choosing simplicity over DRY** — accepting some repetition in endpoint handlers to keep each feature self-contained and obvious
- **Direct EF Core usage** vs. repository pattern — going with direct DbContext injection to reduce indirection while learning

**Maintainability Concerns**:
- As the project grows, consider extracting shared cross-cutting concerns (validation, error handling) into middleware/filters
- The feature-folder pattern scales well but requires discipline about what lives in `/Features` vs. `/Core` or `/Infrastructure`

---

## 4. Chunked Implementation Plan

### **Chunk 1: Project Dependencies & NuGet Packages**
- **Files to touch**: `natura-technical-test.csproj`
- **Nature**: Add package references
- **Outcome**: EF Core, Npgsql (Postgres provider), and tooling packages installed

---

### **Chunk 2: Folder Structure Scaffold**
- **Files to touch**: Create directories (no code yet)
- **Nature**: Create folders
- **Outcome**: Project has the following structure:
  ```
  /Features          # Feature-based vertical slices
  /Core              # Shared domain entities, interfaces
    /Entities
  /Infrastructure    # Database, external services
    /Data            # DbContext, migrations, configurations
      /Configurations
      /Migrations
  /Extensions        # DI registration extensions
  ```

---

### **Chunk 3: Core Entity & DbContext**
- **Files to touch**:
  - Create `/Core/Entities/User.cs`
  - Create `/Infrastructure/Data/AppDbContext.cs`
  - Create `/Infrastructure/Data/Configurations/UserConfiguration.cs` (entity type configuration)
- **Nature**: New files
- **Outcome**: User entity defined, DbContext configured with fluent API

---

### **Chunk 4: Configuration & Connection String**
- **Files to touch**:
  - Modify `appsettings.json`
  - Modify `appsettings.Development.json`
- **Nature**: Add connection strings
- **Outcome**: Postgres connection strings configured for development

---

### **Chunk 5: DI Setup & EF Core Registration**
- **Files to touch**:
  - Create `/Extensions/ServiceCollectionExtensions.cs`
  - Modify `Program.cs`
- **Nature**: Wiring DI container
- **Outcome**: DbContext registered with DI, connection string injected, migrations run on startup (dev only)

---

### **Chunk 6: Migration & Seed Data**
- **Files to touch**:
  - Create initial migration via CLI
  - Create `/Infrastructure/Data/DbInitializer.cs` (seed data)
  - Wire seeding into `Program.cs`
- **Nature**: Database schema creation + sample data
- **Outcome**: Database can be created with `dotnet ef database update`, seed data applied on startup

---

### **Chunk 7: Example Feature Endpoint (Users)**
- **Files to touch**:
  - Create `/Features/Users/GetUsers.cs` (endpoint handler)
  - Create `/Features/Users/UsersEndpoints.cs` (endpoint registration)
  - Modify `Program.cs` to register feature endpoints
- **Nature**: Example vertical slice feature
- **Outcome**: `/api/users` endpoint working, demonstrates the pattern for juniors

---

## Notes

- Project is a simple technical test, not expected to grow
- Keep things straightforward and easy for juniors to understand
- Prioritize clarity over premature optimization
