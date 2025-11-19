# Natura Labs Technical Test

A minimal ASP.NET Core API scaffold for implementing role-based user elevation.

---

## What This Is

This is a technical test for junior candidates. You'll build an endpoint that lets admin users elevate other users to admin role.

The project gives you a working baseline:
- PostgreSQL database with a `User` table
- Seeded users with `"user"` and `"admin"` roles
- Auth abstraction (`ICurrentUserService`) that reads the current user from the `X-User-Id` header
- Two existing endpoints: `GET /api/users` and `GET /api/users/me`

Your job is to add the missing piece.

---

## What You Need to Build

**Endpoint**: `POST /api/users/{id}/elevate`

**Behavior**:
- Only users with `role = "admin"` can call this endpoint
- Non-admins get `403 Forbidden`
- If target user doesn't exist: `404 Not Found`
- If target user is already an admin: `400 Bad Request` with explanation
- On success: update the user's role to `"admin"` and return `200 OK` with the updated user

**Implementation requirements**:
- Use EF Core to query and update the database
- Use `async`/`await` and `SaveChangesAsync`
- Keep the code readable and defensive

**Optional (nice-to-have)**:
- Prevent users from elevating themselves (return `400`)
- Add unit or integration tests
- Document your design choices in a comment or short note

---

## Getting Started

See **[SETUP.md](./SETUP.md)** for instructions on:
- Installing prerequisites (.NET 9, Docker, EF tools)
- Running the database
- Applying migrations
- Starting the API

Once running, you can explore the API at `http://localhost:5000/scalar/v1`.

---

## Project Structure

```
src/
├── Core/
│   ├── Entities/         # Domain models (User)
│   └── Services/         # Abstractions (ICurrentUserService)
├── Infrastructure/
│   ├── Data/             # EF Core DbContext, migrations, seed data
│   └── Services/         # Service implementations
└── Features/
    └── Users/            # User-related endpoints (this is where you'll work)
```

Your endpoint goes in `src/Features/Users/`. Follow the pattern from `GetUsers.cs` and `GetMe.cs`.

---

## Testing Your Work

Use the `X-User-Id` header to simulate different users:

```bash
# As admin (user ID 1):
curl -X POST http://localhost:5000/api/users/2/elevate \
  -H "X-User-Id: 1"

# As regular user (user ID 2):
curl -X POST http://localhost:5000/api/users/3/elevate \
  -H "X-User-Id: 2"
```

Check the seeded users with:
```bash
curl http://localhost:5000/api/users
```

---

## What We're Looking For

- **Correctness**: Does it handle all the requirements and edge cases?
- **Readability**: Is the code clean and easy to follow?
- **Defensive coding**: Do you validate inputs and handle errors properly?
- **EF Core usage**: Do you use the framework idiomatically?

This isn't about perfect code. We want to see how you approach a small, well-defined problem with clear constraints.

---

Good luck! If something's unclear, ask.
