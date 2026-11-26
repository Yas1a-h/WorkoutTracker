# Workout Tracker — ASP.NET Core Web API & Web App

A full-stack CRUD application for tracking gym workouts, exercises, and total lifted volume, built with **ASP.NET Core Web API**, **Entity Framework Core**, and **SQLite**.

## Tech Stack

* **Backend:** C#, ASP.NET Core Web API, Entity Framework Core, SQLite
* **Architecture:** Layered Service Architecture (`IWorkoutService`, `IExerciseService`), Data Transfer Objects (DTOs), Server-Side Validation (`DataAnnotations`)
* **API Documentation:** Swagger / OpenAPI
* **Frontend:** Vanilla HTML5, CSS3, JavaScript (Fetch API) served via `wwwroot`

## Key Features

* **Relational Data Model:** One-to-Many relationship between `Workout` and `Exercise` entities managed via Entity Framework Core migrations.
* **Automated Tonnage Calculation:** Automatically computes per-exercise volume (`WeightKg * Reps`) and aggregates total workout volume (`TotalVolumeKg`) inside the service layer.
* **Live Filtering:** Instant client-side filtering of exercises by muscle group.
* **Input Validation:** Declarative DTO validation preventing negative weights, zero reps, empty titles, or invalid foreign keys with structured `400 Bad Request` responses.

## Project Structure

* `Controllers/` — Thin REST API endpoints (`WorkoutsController`, `ExercisesController`)
* `Data/` — EF Core `AppDbContext` & SQLite configuration
* `DTOs/` — Request & Response models with `DataAnnotations` validation
* `Migrations/` — Entity Framework Core database migrations
* `Models/` — Domain entities (`Workout`, `Exercise`)
* `Services/` — Business logic & DTO mapping (`WorkoutService`, `ExerciseService`)
* `wwwroot/` — Static frontend (`index.html`, `style.css`, `app.js`)
* `Program.cs` — Dependency Injection, static files, and middleware setup

## API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Workouts` | Get all workouts with nested exercises and total volume |
| `GET` | `/api/Workouts/{id}` | Get a specific workout by ID |
| `POST` | `/api/Workouts` | Create a new workout |
| `PUT` | `/api/Workouts/{id}` | Update an existing workout |
| `DELETE` | `/api/Workouts/{id}` | Delete a workout and its exercises |
| `GET` | `/api/Exercises` | Get all exercises (supports `?muscleGroup=` query filter) |
| `POST` | `/api/Exercises` | Add an exercise to an existing workout |
| `PUT` | `/api/Exercises/{id}` | Update an exercise |
| `DELETE` | `/api/Exercises/{id}` | Delete an exercise |

## Getting Started

1. Clone the repository: `git clone https://github.com/Yas1a-h/WorkoutTracker.git`
2. Apply Entity Framework Core migrations: `dotnet ef database update`
3. Run the application: `dotnet run`
4. Open the Web UI at `https://localhost:7197` or Swagger UI at `https://localhost:7197/swagger`