# PostgreSQL Repository Setup

This project uses a simple PostgreSQL repository implementation that stores `Question` entities as JSONB in `dbo.questions`.

Steps to run with Postgres:

1. Install PostgreSQL and create database `reactsim` (or any name).

2. Create schema/table:

```sql
CREATE SCHEMA IF NOT EXISTS dbo;
CREATE TABLE IF NOT EXISTS dbo.questions (
  id SERIAL PRIMARY KEY,
  data jsonb NOT NULL
);
```

3. Configure connection string:

- Set environment variable `POSTGRES_CONNECTION` with a valid connection string, for example:

```
POSTGRES_CONNECTION="Host=localhost;Port=5432;Database=reactsim;Username=postgres;Password=secret"
```

or add to `appsettings.json` under `ConnectionStrings:Postgres`.

4. Run the app and call the services that persist or read questions.

Notes:
- The repository uses `Npgsql` and plain SQL. For production, consider migrations, indexes, and data validation.
- You can change table/column names if needed. The current implementation writes the full `Question` object as JSON into `dbo.questions.data`.
